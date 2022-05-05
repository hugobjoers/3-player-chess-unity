using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public GameObject cell;
    public Board b;
    public string nameOfCell;
    public int xindex;
    public int yindex;
    public int homeBoardOf; //0 if whites, 1 if yellows and 2 if blacks
    public bool color; //true if black, false if not

    public bool occupied; //true if piece on cell, otherwise false

    public int occupant; //0 if white, 1 if yellow, 2 if black




    // Start is called before the first frame update
    void Start()
    {
        nameOfCell = gameObject.name;
        xindex = int.Parse(nameOfCell.Substring(0, 1));
        yindex = int.Parse(nameOfCell.Substring(1, 1));
        homeBoardOf = int.Parse(nameOfCell.Substring(2, 1));
        b.wholeBoard[homeBoardOf][yindex, xindex] = cell;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
