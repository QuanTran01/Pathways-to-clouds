using System.Collections.Generic;
using UnityEngine;

public class CantCloseChest : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;
    public GameObject gem;
    public List<SHARK> sharks;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (gem != null)
        {
            gem.SetActive(false);
        }        
    }

    public void ToggleChest()
    {
        if (!isOpen)
        {
            OpenChest();
        }
    }

    public void OpenChest()
    {
        if (!isOpen)
        {
            animator.SetTrigger("Open");
            isOpen = true;
            if (gem != null)
            {
                gem.SetActive(true);
            }

            if (sharks != null)
            {
                foreach (var shark in sharks)
                {
                    shark.isChestOpened = true; 
                }
            }
        }
    }
}
