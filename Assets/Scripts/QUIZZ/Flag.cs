using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    int unlockedLevels;
    string numberOfLevelsUnlocked = "numberOfLevelsUnlocked";
    bool isFirstTime;
    int offset = 1;
    [SerializeField] Animator transitionAnim;
    private void Start()
    {
        isFirstTime = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isFirstTime)
        {
            UnlockLevel();
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            LoadLevel();
        }
    }   
   IEnumerator LoadLevel()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
      transitionAnim.SetTrigger("Start");
    }
    void UnlockLevel()
    {

        int checkIndex = SceneManager.GetActiveScene().buildIndex;

        unlockedLevels = PlayerPrefs.GetInt(numberOfLevelsUnlocked);

        if (unlockedLevels <= (checkIndex - offset) + 1)
        {
            unlockedLevels += 1;
            PlayerPrefs.SetInt(numberOfLevelsUnlocked, unlockedLevels);
            isFirstTime = false;
        }
    }
}
