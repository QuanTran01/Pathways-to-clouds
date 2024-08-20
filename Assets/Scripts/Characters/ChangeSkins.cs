using UnityEngine;
using UnityEngine.UI;

public class ChangeSkins : MonoBehaviour
{
    public AnimatorOverrideController blueAnim;
    public AnimatorOverrideController pinkAnim;
    public AnimatorOverrideController whiteAnim;
    private Animator anim;

    private int currentSkinIndex;

    private void Start()
    {
        anim = GetComponent<Animator>();
        LoadSkin();
    }

    public void ChangeSkin()
    {
        currentSkinIndex = (currentSkinIndex + 1) % 3;
        SaveAndApplySkin();
    }

    private void SaveAndApplySkin()
    {
        AnimatorOverrideController selectedAnim = blueAnim;
        string skinName = "Blue";

        switch (currentSkinIndex)
        {
            case 0:
                anim.SetTrigger("Blue");
                selectedAnim = blueAnim;
                skinName = "Blue";
                break;
            case 1:
                anim.SetTrigger("Pink");
                selectedAnim = pinkAnim;
                skinName = "Pink";
                break;
            case 2:
                anim.SetTrigger("White");
                selectedAnim = whiteAnim;
                skinName = "White";
                break;
        }

        GetComponent<Animator>().runtimeAnimatorController = selectedAnim as RuntimeAnimatorController;
        PlayerPrefs.SetString("SelectedSkin", skinName);
        PlayerPrefs.SetInt("SkinIndex", currentSkinIndex);
    }

    private void LoadSkin()
    {
        currentSkinIndex = PlayerPrefs.GetInt("SkinIndex", 0);
        SaveAndApplySkin();
    }
}
