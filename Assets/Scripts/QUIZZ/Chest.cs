using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
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
        if (isOpen)
        {
            CloseChest();
        }
        else
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

    public void CloseChest()
    {
        if (isOpen)
        {
            animator.SetTrigger("Close");
            isOpen = false;

            if (gem != null)
            {
                //gem.SetActive(false);
            }

            if (sharks != null)
            {
                foreach (var shark in sharks)
                {
                    shark.isChestOpened = false;  
                }
            }
        }
    }
}
