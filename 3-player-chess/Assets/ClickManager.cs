using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    Vector2 worldPoint;
    private bool nextClickWillMovePiece;
    public float speed;
    private bool moving;
    private GameObject piece;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("piece"))
            {
                piece = hit.collider.gameObject;
                nextClickWillMovePiece = true;
            }
            else if (nextClickWillMovePiece)
            {
                moving = true;
                nextClickWillMovePiece = false;
                //Below is the instant movement version
                // piece.transform.position = worldPoint;
                // float step = speed*Time.deltaTime;
            }
        }
        if (moving && !piece.transform.position.Equals(worldPoint))
        {
            float step = speed * Time.deltaTime;
            piece.transform.position = Vector2.MoveTowards(piece.transform.position, worldPoint, step);
        }
        else
        {
            moving = false;
        }
    }
}
