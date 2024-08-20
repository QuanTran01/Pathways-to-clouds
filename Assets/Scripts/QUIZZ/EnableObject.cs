using UnityEngine;

public class EnableObject : MonoBehaviour
{
    public GameObject[] objectsToToggle; 
    

    void Start()
    {
        SetObjectsActive(false);
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SetObjectsActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SetObjectsActive(false);
        }
    }
    

    private void SetObjectsActive(bool isActive)
    {
        foreach (GameObject obj in objectsToToggle)
        {
            obj.SetActive(isActive);
        }
    }
}
