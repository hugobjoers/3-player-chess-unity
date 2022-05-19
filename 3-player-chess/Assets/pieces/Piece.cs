using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public int homeBoard;
    public Cell currentCell;

    void Start()
    {   
        GameObject b = GameObject.FindGameObjectWithTag("all_pieces");
        gameObject.transform.SetParent(b.transform);
    }
}