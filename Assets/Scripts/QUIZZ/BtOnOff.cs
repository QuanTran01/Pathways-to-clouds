using UnityEngine;
using System.Collections;
public class BtOnOff : MonoBehaviour
{
    public GameObject door; 
    public float doorOpenSpeed = 2f; 
    public Transform doorOpenPosition; 
    private Vector3 doorClosedPosition; 
    private Animator animator;

    private void Start()
    {
        animator=GetComponent<Animator>();
        doorClosedPosition = door.transform.position; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("OtherObject"))
        {
            animator.SetTrigger("start");
            StopAllCoroutines(); 
            StartCoroutine(OpenDoor()); 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("OtherObject"))
        {
            animator.SetTrigger("end");
            StopAllCoroutines();
            StartCoroutine(CloseDoor()); 
        }
    }

    private IEnumerator OpenDoor()
    {
        while (Vector3.Distance(door.transform.position, doorOpenPosition.position) > 0.01f)
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, doorOpenPosition.position, doorOpenSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator CloseDoor()
    {
        while (Vector3.Distance(door.transform.position, doorClosedPosition) > 0.01f)
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, doorClosedPosition, doorOpenSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
