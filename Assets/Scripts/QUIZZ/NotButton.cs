using UnityEngine;
using System.Collections;

public class NotButton : MonoBehaviour
{
    public GameObject objectt;
    public float objectMoveSpeed = 4f;
    public Transform targetPosition;
    private Vector3 startPosition;
    private bool isMove = false;

    private void Start()
    {
        startPosition = objectt.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player") || collision.CompareTag("OtherObject")) && !isMove)
        {
            StartCoroutine(MoveCloud());
        }
    }

    private IEnumerator MoveCloud()
    {
        while (Vector3.Distance(objectt.transform.position, targetPosition.position) > 0.01f)
        {
            objectt.transform.position = Vector3.MoveTowards(objectt.transform.position, targetPosition.position, objectMoveSpeed * Time.deltaTime);
            yield return null;
        }
        isMove = true;
    }
}