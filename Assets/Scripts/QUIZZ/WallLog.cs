using UnityEngine;

public class WallLog : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private bool isFalling = false;
    public float fallSpeed = 5f; 

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isFalling)
        {
            rb2d.rotation = Mathf.Lerp(rb2d.rotation, -90f, Time.deltaTime * fallSpeed);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isFalling = true;
        }
    }
}
