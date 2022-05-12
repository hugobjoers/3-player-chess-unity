using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{   
    public GameObject cellPrefab;
    public double sizeOfBoard = 9.4;
    private double[] angles = new double[]{Mathf.PI*3/2, Mathf.PI*5/6, Mathf.PI*1/6};
    private double[,] xNotTransposed = new double[,]{{0.7,1.8,2.9,4.2},{1.2,2.2,3.2,4.2},{1.8,2.6,3.4,4.2},{2.4,3.0,3.6,4.2}};
    private double[,] yNotTransposed = new double[,]{{0.4,1.4,2.4,3.4},{0.4,1.3,2.2,3.1},{0.4,1.2,2.0,2.8},{0.4,1.1,1.8,2.5}};

    public GameObject[,,] wholeBoard; //in order white,yellow,black

    // Start is called before the first frame update
    void Start()
    {
        wholeBoard = new GameObject[3,8,4];
        InitialiseBoard(wholeBoard);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void InitialiseBoard(GameObject[,,] wholeBoard)
    {    
        for(int b = 0; b < 3; b++)
        {
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0;j<8;j++)
                {   
                    double[] pos = Pos(b,i,j); 
                    wholeBoard[b,j,i] = Instantiate(cellPrefab, new Vector2((float)pos[0],(float)pos[1]), Quaternion.identity);
                    wholeBoard[b,j,i].tag = "cell";
                    wholeBoard[b,j,i].name = b.ToString() + j.ToString() + i.ToString();
                }
            }
        }
    }


    double[] Pos(int b, int i, int j)
    {
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
