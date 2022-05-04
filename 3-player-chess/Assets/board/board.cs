using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject[,] wBoard;

    // Start is called before the first frame update
    void Start()
    {
        wBoard = new GameObject[4,8];
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(wBoard[0,0]);
    }
}
