using UnityEngine;
using System.Collections;

public class ButtonCanvas : MonoBehaviour
{
    public GameObject objectt;
    public float objectMoveSpeed = 4f;
    public Transform targetPosition;
    private Vector3 startPosition;
    private Animator animator;
    private bool isMove = false;

    private void Start()
    {
        startPosition = objectt.transform.position;
        animator = GetComponent<Animator>(); 
    }

    // Phương thức này sẽ được gọi khi nút trên Canvas được nhấn
    public void StartMoving()
    {
        if (!isMove)
        {
            animator.SetTrigger("start");
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
