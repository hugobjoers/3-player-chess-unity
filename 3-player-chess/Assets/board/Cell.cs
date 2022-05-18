using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Class for modeling a cell in chess board
public class Cell : MonoBehaviour
{
    public GameObject b;
    //refrence to the board of the game

    public string nameOfCell;
    public int xindex;
    public int yindex;
    //xindex and yindex in terms of relative position in homeboard 
    //xindex goes from 0(left) to 7(right) and y goes from 0 (top) to 3 (bottom)

    public int homeBoard; 
    //signifies which third of board cell is on
    //0 if whites, 1 if yellows and 2 if blacks

    public bool occupied; 
    //true if piece is on cell, otherwise false

    public int occupant; 
    //signifies the colour of occupiying piece
    //0 if white, 1 if yellow, 2 if black




    // Start is called before the first frame update
    void Start()
    {   
        nameOfCell = gameObject.name;
        //Cell will be instantiated by Board, hence Cell already has a name
        
        homeBoard = int.Parse(nameOfCell.Substring(0,1));
        xindex = int.Parse(nameOfCell.Substring(1,1));
        yindex = int.Parse(nameOfCell.Substring(2,1));
        b = GameObject.FindGameObjectWithTag("board");
        gameObject.transform.SetParent(b.transform);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
