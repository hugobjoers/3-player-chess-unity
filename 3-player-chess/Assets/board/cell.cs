using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int xindex;
    public int yindex;
    public Board b;
    public bool color; //true if black, false if not

    
    // Start is called before the first frame update
    void Start()
    {
        b.wBoard[xindex, yindex] = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
