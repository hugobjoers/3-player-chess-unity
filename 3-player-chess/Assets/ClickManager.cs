using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    Vector3 worldPoint;
    private bool nextClickWillMovePiece;
    public float speed;
    private bool moving;
    private GameObject piece;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //https://answers.unity.com/questions/598492/how-do-you-set-an-order-for-2d-colliders-that-over.html
            worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPoint.z = Camera.main.transform.position.z;
            Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("piece"))
            {
                piece = hit.collider.gameObject;
                nextClickWillMovePiece = true;
                Debug.Log("clicked piece");
            }
            else if (nextClickWillMovePiece)
            {
                if (IsLegalMove(hit, piece))
                {
                    piece.transform.position = new Vector3(worldPoint.x, worldPoint.y, piece.transform.position.z);
                }
                nextClickWillMovePiece = false;
            }
        }
    }

    bool IsLegalMove(RaycastHit2D hit, GameObject piece)
    {
        GameObject cellObject = hit.collider.gameObject;
        Cell cell = cellObject.GetComponent<Cell>();
        if (piece.name.Contains("pawn"))
        {
            cell = MoveDown(cell);
            if (IsEmpty(cell))
            {
                BoxCollider2D bc = cell.GetComponent<BoxCollider2D>();
                Collider2D[] results = new Collider2D[5];
                ContactFilter2D cf = new ContactFilter2D().NoFilter();
                bc.OverlapCollider(cf, results);
                foreach (Collider2D obj in results)
                {
                    if (obj.gameObject == piece)
                    {
                        return true;
                    }
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
        Board b = cell.b.GetComponent<Board>();
        Cell newCell = b.wholeBoard[cell.homeBoardOf, cell.xindex, cell.yindex + 1].GetComponent<Cell>();
        return newCell;

    }

    bool pawnMovement(Cell cell)
    {
        cell = MoveDown(cell);
        if (IsEmpty(cell))
        {
            BoxCollider2D bc = cell.GetComponent<BoxCollider2D>();
            Collider2D[] results = new Collider2D[5];
            ContactFilter2D cf = new ContactFilter2D().NoFilter();
            bc.OverlapCollider(cf, results);
            foreach (Collider2D obj in results)
            {
                if (obj.gameObject == piece)
                {
                    return true;
                }
            }
        }
        return false;
    }

}
