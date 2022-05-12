using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public int homeBoard;
    void Start()
    {
        if(gameObject.name.Contains("white"))
        {
            homeBoard = 0;
        }
        else if(gameObject.name.Contains("black"))
        {
            homeBoard = 1;
        }
        else if(gameObject.name.Contains("yellow"))
        {
            homeBoard = 2;
        }
    }
}