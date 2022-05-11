using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{   
    public GameObject cellPrefab;
    public GameObject[,] wBoard;
    public GameObject[,] yBoard;
    public GameObject[,] bBoard;
    public double sizeOfBoard = 9.4;
    private double[] angles = new double[]{Mathf.PI*3/2, Mathf.PI*5/6, Mathf.PI*1/6};
    private double[,] xNotTransposed = new double[,]{{0.7,1.8,2.9,4.2},{1.2,2.2,3.2,4.2},{1.8,2.6,3.4,4.2},{2.4,3.0,3.6,4.2}};
    private double[,] yNotTransposed = new double[,]{{0.4,1.4,2.4,3.4},{0.4,1.3,2.2,3.1},{0.4,1.2,2.0,2.8},{0.4,1.1,1.8,2.5}};

    public GameObject[][,] wholeBoard;

    // Start is called before the first frame update
    void Start()
    {
        wBoard = new GameObject[4,8];
        yBoard = new GameObject[4,8];
        bBoard = new GameObject[4,8];
        wholeBoard = new GameObject[3][,]{wBoard, yBoard, bBoard};
        InitialiseBoard(wholeBoard);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(wBoard[0,0]);
    }

    void InitialiseBoard(GameObject[][,] wholeBoard)
    {    
        for(int b = 0; b < 3; b++)
        {
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0;j<8;j++)
                {   
                    double[] pos = Pos(b,i,j); 
                    wholeBoard[b][i,j] = Instantiate(cellPrefab, new Vector2((float)pos[0],(float)pos[1]), Quaternion.identity);
                    wholeBoard[b][i,j].tag = "cell";
                    wholeBoard[b][i,j].name = b.ToString() + i.ToString() + j.ToString();
                }
            }
        }
    }


    double[] Pos(int b, int i, int j)
    {

/*      (tried to make formulas for positions)
        double yNotTransformedRight = 0.4 -(j-4)*(i/10);
        double yNotTransformedLeft = -0.4 -(j-3)*(i/10);
        double xNotTransformedRight = 4.2 + (i-3)*(1.2-(j-4)*0.2);
        double xNotTransformedLeft = 4.2 + (i-3)*(1.2-(j-3)*0.2); */
        double xPreT;
        double yPreT;
        if(j>3)
        {
            xPreT = xNotTransposed[j-4,i];
            yPreT = yNotTransposed[i,j-4];
        }
        else
        {
            xPreT = xNotTransposed[3-j,i];
            yPreT = -1*yNotTransposed[i,3-j];
        }
        double angle = angles[b];

        double x = xPreT*Mathf.Cos((float) angle) -yPreT*Mathf.Sin((float) angle);
        double y = xPreT*Mathf.Sin((float) angle) +yPreT*Mathf.Cos((float) angle);
        double[] pos = new double[]{x,y};
        return pos;
    }

}
