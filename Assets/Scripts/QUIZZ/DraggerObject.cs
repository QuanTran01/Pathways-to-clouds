using UnityEngine;

public class DraggerObject : MonoBehaviour
{
    private Vector3 touchOffset;
    private bool isDragging = false;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                    if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                    {
                        touchOffset = (Vector2)transform.position - touchPos;
                        isDragging = true;
                    }
                    break;

                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        Vector2 newPos = Camera.main.ScreenToWorldPoint(touch.position) + touchOffset;
                        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
                    }
                    break;

                case TouchPhase.Ended:
                    isDragging = false;
                    break;
            }
        }
    }
}
