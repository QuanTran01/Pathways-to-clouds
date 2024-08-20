using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button sfxButton;

    [SerializeField] Image musicIconOn;
    [SerializeField] Image musicIconOff;
    [SerializeField] Image sfxIconOn;
    [SerializeField] Image sfxIconOff;

    


    private bool musicMuted = false;
    private bool sfxMuted = false;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicMuted"))
        {
            Load();
        }
        UpdateMusicButtonIcon();
        musicButton.onClick.AddListener(ToggleMusic);
        sfxButton.onClick.AddListener(ToggleSfx);

    }

    public void ToggleMusic()
    {      

        musicMuted = !musicMuted;
        myMixer.SetFloat("music", musicMuted ? -80f : 0f);
   
        PlayerPrefs.Save();
        Save();
        UpdateMusicButtonIcon();
        
    }

    public void ToggleSfx()
    {
        sfxMuted = !sfxMuted;
        myMixer.SetFloat("sfx", sfxMuted ? -80f : 0f); 
       
        PlayerPrefs.Save();      
        UpdateSfxButtonIcon();
        Save();
    }
    private void UpdateMusicButtonIcon()
    {
        if(musicMuted==false)
        {
            musicIconOn.enabled = true;
            musicIconOff.enabled = false;
        }
        else
        {
            musicIconOn.enabled = false;
            musicIconOff.enabled = true;
        }
    }
    private void UpdateSfxButtonIcon()
    {
        if (sfxMuted == false)
        {
            sfxIconOn.enabled = true;
            sfxIconOff.enabled = false;
        }
        else
        {
            sfxIconOn.enabled = false;
            sfxIconOff.enabled = true;
        }
    }
    private void Load()
    {
        musicMuted = PlayerPrefs.GetInt("musicMuted") == 1;
        sfxMuted = PlayerPrefs.GetInt("sfxMuted") == 1;
        myMixer.SetFloat("music", musicMuted ? -80f : 0f);
        myMixer.SetFloat("sfx", sfxMuted ? -80f : 0f);
        
    }
    private void Save()
    {
        PlayerPrefs.SetInt("musicMuted", musicMuted ? 1 : 0);
        PlayerPrefs.SetInt("sfxMuted", sfxMuted ? 1 : 0);
    }

    
}
