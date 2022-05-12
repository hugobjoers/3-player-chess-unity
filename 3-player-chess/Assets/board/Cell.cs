using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cell : MonoBehaviour
{
    public GameObject b;
    public string nameOfCell;
    public int xindex;
    public int yindex;
    public int homeBoardOf; //0 if whites, 1 if yellows and 2 if blacks
    public bool occupied; //true if piece on cell, otherwise false

    public int occupant; //0 if white, 1 if yellow, 2 if black




    // Start is called before the first frame update
    void Start()
    {
        nameOfCell = gameObject.name;
        homeBoardOf = int.Parse(nameOfCell.Substring(0,1));
        xindex = int.Parse(nameOfCell.Substring(1,1));
        yindex = int.Parse(nameOfCell.Substring(2,1));
        b = GameObject.FindGameObjectWithTag("board");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
