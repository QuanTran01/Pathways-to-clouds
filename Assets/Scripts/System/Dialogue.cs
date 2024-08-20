using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public GameObject window;
    public GameObject indicator;
    public List<TMP_Text> dialogues;
    public float writingSpeed;
    public float delayBetweenDialogues = 0.5f;

    private int index;
    private int charIndex;

    public bool started { get; private set; }
    public bool displayingDialogue;

    public delegate void DialogueEnded();
    public event DialogueEnded OnDialogueEnded;

    private void Awake()
    {
        ToggleIndicator(false);
        ToggleWindow(false);

        foreach (var dialogue in dialogues)
        {
            if (dialogue != null)
            {
                dialogue.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("Dialogue is null in the dialogues list.");
            }
        }
    }

    private void ToggleWindow(bool show)
    {
        if (window != null)
        {
            window.SetActive(show);
        }
        else
        {
            Debug.LogWarning("Window is not assigned.");
        }
    }

    public void ToggleIndicator(bool show)
    {
        if (indicator != null)
        {
            indicator.SetActive(show);
        }
        else
        {
            Debug.LogWarning("Indicator is not assigned.");
        }
    }

    public void StartDialogue()
    {
        if (started)
            return;
        started = true;
        ToggleWindow(true);
        ToggleIndicator(false);
        index = 0;
        displayingDialogue = true;
        StartCoroutine(Writing());
    }

    public void EndDialogue()
    {
        started = false;
        ToggleWindow(false);

        OnDialogueEnded?.Invoke();
    }

    private IEnumerator Writing()
    {
        if (index < dialogues.Count && dialogues[index] != null)
        {
            TMP_Text currentDialogue = dialogues[index];
            currentDialogue.gameObject.SetActive(true);
            charIndex = 0;
            string text = currentDialogue.text;
            currentDialogue.text = string.Empty;
            while (charIndex < text.Length)
            {
                yield return new WaitForSeconds(writingSpeed);
                currentDialogue.text += text[charIndex];
                charIndex++;
            }
            yield return new WaitForSeconds(delayBetweenDialogues);
            NextDialogue();
        }
        else
        {
            Debug.LogError("Dialogue at index " + index + " is null or index is out of range.");
        }
    }

    public void NextDialogue()
    {
        if (index < dialogues.Count)
        {
            dialogues[index].gameObject.SetActive(false);
        }

        index++;
        if (index < dialogues.Count)
        {
            displayingDialogue = true;
            StartCoroutine(Writing());
        }
        else
        {
            ToggleIndicator(true);
            EndDialogue();
        }
    }

    private void Update()
    {
        if (!started || displayingDialogue)
            return;
    }
}
