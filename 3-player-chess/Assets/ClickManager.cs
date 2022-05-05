using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    Vector2 worldPoint;
    private bool nextClickWillMovePiece;
    public float speed;
    private bool moving;
    private GameObject piece;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("piece"))
            {
                piece = hit.collider.gameObject;
                nextClickWillMovePiece = true;
            }
            else if (nextClickWillMovePiece)
            {
                if (IsLegalMove(hit, piece))
                {
                    moving = true;
                    //Below is the instant movement version
                    // piece.transform.position = worldPoint;
                    // float step = speed*Time.deltaTime;
                }
                nextClickWillMovePiece = false;
            }
        }
        if (moving && !piece.transform.position.Equals(worldPoint))
        {
            float step = speed * Time.deltaTime;
            piece.transform.position = Vector2.MoveTowards(piece.transform.position, worldPoint, step);
        }
        else
        {
            moving = false;
        }
    }

    bool IsLegalMove(RaycastHit2D hit, GameObject piece)
    {
        GameObject cellObject = hit.collider.gameObject;
        Cell cell = cellObject.GetComponent("Cell") as Cell;
        if (piece.name.Contains("pawn"))
        {
            if (IsEmpty(MoveDown(cell)))
            {
                BoxCollider2D bc = cell.cell.GetComponent("Box Collider 2D") as BoxCollider2D;
                Collider2D[] results = new Collider2D[1]; 
                ContactFilter2D cf = new ContactFilter2D().NoFilter();
                bc.OverlapCollider(cf, results);
                if(results[0].gameObject == piece)
                {
                    return true;
                }
            }
        }
        return false;
    }

    bool IsEmpty(Cell cell)
    {
        if (cell == null)
        {
            return false;
        }
        return !cell.occupied;
    }

    Cell MoveDown(Cell cell)
    {
        Debug.Log(cell);
        Cell newCell = cell.b.wholeBoard[cell.homeBoardOf][cell.yindex, cell.xindex].GetComponent("Cell") as Cell;
        Debug.Log("newCell" + newCell);
        return newCell;
    }

}
