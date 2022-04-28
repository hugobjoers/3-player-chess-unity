using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pawn_movement : MonoBehaviour//, IPointerClickHandler
{
    public float speed = 5f;
    Vector2 lastClickedPos;
    bool moving;

    // Update is called once per frame
    void Update()
    {
        while (moving)
        {
            lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (moving && (Vector2)transform.position != lastClickedPos)
            {
                float step = speed * Time.deltaTime;
                Debug.Log("moving");
                transform.position = Vector2.MoveTowards(transform.position, lastClickedPos, step);
            }
            else
            {
                moving = false;
            }
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("mousedown");
        StartCoroutine(Wait());
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        moving = true;
    }
}
