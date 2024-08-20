using UnityEngine;

public class SAW : MonoBehaviour
{
    public float speed = 90.0f;
    void Update()
    {
        transform.Rotate(0, 0, speed * Time.deltaTime);
    }
}
