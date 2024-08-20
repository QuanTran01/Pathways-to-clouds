using UnityEngine;
using UnityEngine.UI;

public class ButtonsHandler : MonoBehaviour
{
    [SerializeField] Button[] levelButtons;
    string numberOfLevelsUnlocked = "numberOfLevelsUnlocked";
    int unlockedLevels;
    int oldUnlockedLevels;


    private void Start()
    {
        if (!PlayerPrefs.HasKey(numberOfLevelsUnlocked))
        {   
            PlayerPrefs.SetInt(numberOfLevelsUnlocked, 1);
        }
        unlockedLevels = PlayerPrefs.GetInt(numberOfLevelsUnlocked);
        oldUnlockedLevels = unlockedLevels;

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i < unlockedLevels)
            {
                levelButtons[i].interactable = true;
            }
            else
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    private void Update()
    {
        unlockedLevels = PlayerPrefs.GetInt(numberOfLevelsUnlocked);
        if (oldUnlockedLevels != unlockedLevels) {
            for (int i = 0; i < levelButtons.Length; i++)
            {
                if (i < unlockedLevels)
                {
                    levelButtons[i].interactable = true;
                }
                else
                {
                    levelButtons[i].interactable = false;
                }
            }
            oldUnlockedLevels = unlockedLevels;
        }
    }
}
