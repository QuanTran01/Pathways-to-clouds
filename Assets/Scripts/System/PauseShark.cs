using UnityEngine;

public class PauseShark : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject hintPanel;
    private SHARK[] sharks;

    public void Pause()
    {
        pausePanel.SetActive(true);
        SetSharkActiveState(false);
    }
    public void Hint()
    {
        Time.timeScale = 0f;
        hintPanel.SetActive(true);
    }

    public void Continue()
    {
        pausePanel.SetActive(false);
        SetSharkActiveState(true);
        hintPanel.SetActive(false);
    }

    private void SetSharkActiveState(bool isActive)
    {
        SHARK[] sharks = FindObjectsOfType<SHARK>();
        foreach (SHARK shark in sharks)
        {
            shark.enabled = isActive;
        }
    }
}
