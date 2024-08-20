using UnityEngine;

public class Lock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Key")) 
        {
            Destroy(gameObject); 
        }
    }
}
