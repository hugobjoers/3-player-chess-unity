using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class for modeling the Chess board
public class Board : MonoBehaviour
{   
    public GameObject cellPrefab;

    private double[] angles = new double[]{Mathf.PI*3/2, Mathf.PI*5/6, Mathf.PI*1/6}; 
    //angles is an array of the angles of the rotation for the center line of each homeboard around origo

    private double[,] xNotTransposed = new double[,]{{0.7,1.8,2.9,4.2},{1.2,2.2,3.2,4.2},{1.8,2.6,3.4,4.2},{2.4,3.0,3.6,4.2}};
    private double[,] yNotTransposed = new double[,]{{0.4,1.4,2.4,3.4},{0.4,1.3,2.2,3.1},{0.4,1.2,2.0,2.8},{0.4,1.1,1.8,2.5}};
    //xNotTransposed and yNotTransposed are the coordinates of all cells in the right half of an imagined homeboard with rotation 0

    public GameObject[,,] wholeBoard;
    //wholeBoard is the main model of the board, which will contain arrays of arrays of arrays of Cells (as GameObjects)
    //in the three player chess version that we have choosed to do, each third of the board is basically just one half of
    //a normal chess board, so wholeBoard is an array of three halves of a normal chessboard, which are in turn arrays of arrays
    //to get x and y coordinates of each cell
    //we symbolize the whites homeboard as 0, yellows as 1 and blacks as 2
    //y goes from 3 at the bottom and to 0 at the top (because this is how you would see an array that was written in code)
    //x goes from 0 at the leftmost cells to 7 at the rightmost
    //for example: to get the cell in the lefthand corner of the whites homeboard you would write wholeBoard[0,0,3]

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


    //Method for initialising the board e.g. creating all
    //cells and storing them in the wholeBoard field
    void InitialiseBoard(GameObject[,,] wholeBoard)
    {    
        for(int b = 0; b < 3; b++) //board index
        {
            for(int i = 0; i < 4; i++) //y index
            {
                for(int j = 0; j < 8; j++) //x index
                {   
                    double[] pos = Pos(b,j,i); 
                    wholeBoard[b,j,i] = Instantiate(cellPrefab, new Vector2((float)pos[0],(float)pos[1]), Quaternion.identity);
                    //creates GameObjects in the game at position pos in the image of cellPrefab and stores thiese GameObjects in wholeBoard

                    wholeBoard[b,j,i].tag = "cell";
                    wholeBoard[b,j,i].name = b.ToString() + j.ToString() + i.ToString();
                }
            }
        }
    }

    //Method for getting the correct in game positions of cells,
    //based on their placements in the board
    double[] Pos(int b, int j, int i)
    {
        double xPreT;
        double yPreT;

        if(j>3) //we are on the right hand side of the homeboard
        {   
            xPreT = xNotTransposed[j-4,i]; 
            yPreT = yNotTransposed[i,j-4];
            //since j (x) goes from 4 to 7
            //and the x index in these arrays
            //goes from 0 to 3
        }
        else //we are on the left hand side of the homeboard
        {
            xPreT = xNotTransposed[3-j,i];
            yPreT = -1*yNotTransposed[i,3-j];
            //j (x) goes from 0 to 3, and we want
            //the opposite order (mirrored) since
            //the arrays model the rigth hand side
            //also we want the negative y values
            //sine we are mirroring in the line y=0 
        }
        double angle = angles[b];

        double x = xPreT*Mathf.Cos((float) angle) -yPreT*Mathf.Sin((float) angle);
        double y = xPreT*Mathf.Sin((float) angle) +yPreT*Mathf.Cos((float) angle);
        //this is standard operations for rotating an vector around origo in the xy-plane with given angle
        
        double[] pos = new double[]{x,y};
        return pos;
    }

}
