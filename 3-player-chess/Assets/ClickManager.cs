using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public float speed = 5f;
    private bool nextClickWillMovePiece;
    private GameObject piece;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("piece"))
            {
                piece = hit.collider.gameObject;
                nextClickWillMovePiece = true;
            }
            else if (nextClickWillMovePiece)
            {
                piece.transform.position = worldPoint;
                nextClickWillMovePiece = false;
            }
        }

    }
}
