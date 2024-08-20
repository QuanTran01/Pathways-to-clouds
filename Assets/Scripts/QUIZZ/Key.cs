using UnityEngine;

public class Key  : MonoBehaviour
{
    public Vector3 offset = new Vector3(1f, 0, 0); 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.SetParent(collision.transform);
            transform.localPosition = Vector3.zero + offset;
            GetComponent<Collider2D>().isTrigger = false;
        }
    }
}
