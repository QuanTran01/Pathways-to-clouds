using System.Security.Cryptography;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject hintPanel;
    private RewardedAdsButton rewardedAdsButton;
    public GameObject adImage;
    
    public void OnYesButtonClicked()
    {
       // adImage.SetActive(false);
        rewardedAdsButton.ShowAd();
    }
    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Hint()
    {
        Time.timeScale = 0f;
        hintPanel.SetActive(true);
    }

    public void Continue()
    {
        pausePanel.SetActive(false);
        hintPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void OnAdCompleted()
    {
        Debug.Log("Ad completed. Hiding ad image.");
        adImage.SetActive(false);
    }
}
