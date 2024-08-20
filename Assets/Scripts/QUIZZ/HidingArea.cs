using UnityEngine;

public class HidingArea : MonoBehaviour
{
    public GameObject hidingArea;  

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {           
                hidingArea.SetActive(false);           
        }
    }
 }
    
