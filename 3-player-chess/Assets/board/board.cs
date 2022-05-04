using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{   
    public GameObject cellPrefab;
    public GameObject[,] wBoard;
    public GameObject[,] yBoard;
    public GameObject[,] bBoard;

    public GameObject[][,] wholeBoard;

    // Start is called before the first frame update
    void Start()
    {
        wBoard = new GameObject[4,8];
        yBoard = new GameObject[4,8];
        bBoard = new GameObject[4,8];
        wholeBoard = new GameObject[3][,]{wBoard, yBoard, bBoard};
        initialiseBoard(wholeBoard);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(wBoard[0,0]);
    }

    void initialiseBoard(GameObject[][,] wholeBoard)
    {    
        for(int b = 0; b < 3; b++)
        {
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0;j<8;j++)
                {   
                    if(b==0)
                    {
                    wholeBoard[b][i,j] = Instantiate(cellPrefab, new Vector2(-4+j,-4+i), Quaternion.identity);
                    wholeBoard[b][i,j].tag = "cell";
                    wholeBoard[b][i,j].name = b.ToString() + i.ToString() + j.ToString();
                    }
                    else
                    {

                    }
                }
            }
        }
    }

}
