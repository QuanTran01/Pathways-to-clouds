using UnityEngine;
public class Brick : MonoBehaviour
{
    public float holdDuration = 3f;
    private bool isBeingHeld = false;
    private float holdStartTime;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                    {
                        holdStartTime = Time.time;
                        isBeingHeld = true;
                        animator.SetBool("isShaking", true);
                    }
                    break;

                case TouchPhase.Ended:
                    isBeingHeld = false;
                    animator.SetBool("isShaking", false);
                    break;
            }
        }

        if (isBeingHeld && (Time.time - holdStartTime >= holdDuration))
        {
            DestroyBrick();
        }
    }

    private void DestroyBrick()
    {
        animator.SetTrigger("broke");
        Destroy(gameObject, 0.5f);
    }
}
