using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogueScript;
    public float detectionRadius = 2f;
    private Transform playerTransform;
    private SpriteRenderer spriteRenderer;
    private bool dialogueStarted;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        dialogueStarted = false;
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            float distance = Vector3.Distance(transform.position, playerTransform.position);
            if (distance < detectionRadius && !dialogueStarted)
            {
                dialogueScript.ToggleIndicator(true);
                dialogueScript.StartDialogue();
                dialogueStarted = true;

                Vector3 direction = playerTransform.position - transform.position;
                if (direction.x > 0)
                {
                    spriteRenderer.flipX = false;
                }
                else if (direction.x < 0)
                {
                    spriteRenderer.flipX = true;
                }
            }
            else if (distance >= detectionRadius)
            {
                if (dialogueStarted)
                {
                    dialogueScript.ToggleIndicator(false);
                    dialogueScript.EndDialogue();
                    dialogueStarted = false;
                }
            }
        }
    }
}
