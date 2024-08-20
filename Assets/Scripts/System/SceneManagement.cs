using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void StartGame()
    {
         SceneManager.LoadScene("Level Menu");      
    }
    public void quit()
    {
        Application.Quit();
    }
    public void skin()
    {
        SceneManager.LoadScene("Skin");

    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f;

    }
    public void setting()
    {
        SceneManager.LoadScene("Setting Menu");
    }

}
