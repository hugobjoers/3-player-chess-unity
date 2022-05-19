using System.Collections;
using System.Collections.Generic;
using System;
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
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero); //Positioin of the mouse cursor
            if (hit.collider == null)
            {
                return;
            }
            Debug.Log(hit.collider.gameObject);
            if (hit.collider != null && hit.collider.CompareTag("piece"))
            {
                piece = hit.collider.gameObject;
                nextClickWillMovePiece = true;
                PieceZPos(5);
            }
            else if (nextClickWillMovePiece)
            {
                nextClickWillMovePiece = false;
                PieceZPos(-5);
                if (IsLegalMove(hit, piece))
                {
                    piece.transform.position = new Vector3(worldPoint.x, worldPoint.y, piece.transform.position.z);
                }
            }
            else
            {
                nextClickWillMovePiece = false;
                PieceZPos(-5);
            }
        }
    }
    ///<summary>
    ///Sets all pieces z-position to the parameter
    ///</summary>
    void PieceZPos(int z)
    {
        GameObject[] pieces = GameObject.FindGameObjectsWithTag("piece");
        foreach (GameObject obj in pieces)
        {
            Vector3 pos = obj.transform.position;
            obj.transform.position = new Vector3(pos.x, pos.y, z);
        }
    }

    bool IsLegalMove(RaycastHit2D hit, GameObject piece)
    {
        GameObject cellObject = hit.collider.gameObject;
        Cell cell = cellObject.GetComponent<Cell>();
        if (piece.name.Contains("pawn"))
        {
            return PawnMovement(cell, piece);
        }
        else if (piece.name.Contains("king"))
        {
            Debug.Log("kingmovement");
            return KingMovement(cell, piece);
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
    ///<returns>
    ///The cell one position down. What is considered down depends on toBoard. If toBoard is 0, 0 is the board direction that is considered down.
    ///</returns>
    Cell MoveDown(Cell cell, int toBoard)
    {
        if (cell == null)
        {
            return null;
        }
        Board b = cell.b.GetComponent<Board>();
        if (cell.yindex == 0 && cell.homeBoard != toBoard) //Crossing into another board
        {
            return b.wholeBoard[toBoard, 7 - cell.xindex, 0].GetComponent<Cell>();

        }
        if (cell.homeBoard != toBoard) //On another board
        {
            return b.wholeBoard[cell.homeBoard, cell.xindex, cell.yindex - 1].GetComponent<Cell>();
        }
        return b.wholeBoard[cell.homeBoard, cell.xindex, cell.yindex + 1].GetComponent<Cell>(); //On homeBoard
    }

    ///<returns>
    ///The cell one position to the left.
    ///</returns>
    Cell MoveLeft(Cell cell, int toBoard)
    {
        Board b = cell.b.GetComponent<Board>();
        try
        {
            if (cell.homeBoard != toBoard)
            {
                return b.wholeBoard[cell.homeBoard, cell.xindex + 1, cell.yindex].GetComponent<Cell>();
            }
            else
            {
                return b.wholeBoard[cell.homeBoard, cell.xindex - 1, cell.yindex].GetComponent<Cell>();
            }
        }
        catch (System.IndexOutOfRangeException)
        {
            return null;
        }
    }
    ///<returns>
    ///The cell one position to the right.
    ///</returns>
    Cell MoveRight(Cell cell, int toBoard)
    {
        Board b = cell.b.GetComponent<Board>();
        try
        {
            if (cell.homeBoard != toBoard)
            {
                Debug.Log(b.wholeBoard[cell.homeBoard, cell.xindex - 1, cell.yindex]);
                return b.wholeBoard[cell.homeBoard, cell.xindex - 1, cell.yindex].GetComponent<Cell>();
            }
            else
            {
                return b.wholeBoard[cell.homeBoard, cell.xindex + 1, cell.yindex].GetComponent<Cell>();
            }
        }
        catch (System.IndexOutOfRangeException)
        {
            return null;
        }
    }

    bool PawnMovement(Cell cell, GameObject piece)
    {
        int toBoard = piece.GetComponent<Piece>().homeBoard;
        bool onm = PawnOnlyMovement(cell, piece);
        bool take = PawnTake(cell, piece, MoveDown(MoveLeft(cell, toBoard), toBoard)) || PawnTake(cell, piece, MoveDown(MoveRight(cell, toBoard), toBoard));
        return onm || take;
    }

    ///<summary>
    ///Checks if moving upwards is legal
    ///</summary>
    bool PawnOnlyMovement(Cell cell, GameObject piece)
    {
        int toBoard = piece.GetComponent<Piece>().homeBoard;
        Cell newCell = MoveDown(cell, toBoard);
        Debug.Log("down newCell" + newCell);
        if (newCell == null)
        {
            return false;
        }
        if (IsEmpty(cell))
        {
            Collider2D[] results = Collisions(newCell);
            foreach (Collider2D obj in results)
            {
                if (obj == null)
                {
                    return false;
                }
                if (obj.gameObject == piece)
                {
                    cell.occupied = true;
                    newCell.occupied = false;
                    return true; //Legal forward move
                }
            }
        }
        return false;
    }
    ///<returns>
    ///The colliders that overlap with the parameter.
    ///</returns>
    Collider2D[] Collisions(Cell newCell)
    {
        BoxCollider2D bc = newCell.GetComponent<BoxCollider2D>();
        Collider2D[] results = new Collider2D[5];
        ContactFilter2D cf = new ContactFilter2D().NoFilter();
        bc.OverlapCollider(cf, results);
        return results;
    }

    /// <summary>
    /// Checks if taking is legal
    /// </summary>
    bool PawnTake(Cell cell, GameObject piece, Cell newCell)
    {
        if (newCell == null)
        {
            return false;
        }
        int toBoard = newCell.homeBoard;
        Collider2D[] results = Collisions(newCell);
        bool validMove = false;
        foreach (Collider2D obj in results)
        {
            if (obj != null && obj.gameObject == piece)
            {
                validMove = true;
            }
        }
        if (validMove)
        {
            DestroyEnemyInCell(cell, newCell, piece.GetComponent<Piece>().homeBoard);
            return true;
        }
        return false;
    }


    bool KingMovement(Cell cell, GameObject piece)
    {
        return PawnMovement(cell, piece);
    }


    void DestroyEnemyInCell(Cell cell, Cell newCell, int takingColor)
    {
        Collider2D[] results = Collisions(cell);
        foreach (Collider2D obj in results)
        {
            if (obj == null)
            {
                continue;
            }
            if (obj.CompareTag("piece") && obj.gameObject.GetComponent<Piece>().homeBoard != takingColor) //is an enemy piece
            {
                Destroy(obj.gameObject);
                newCell.occupied = false;
            }
        }
    }

}
