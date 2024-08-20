using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------------ Audio Source ------------")]
    [SerializeField] AudioSource soundSource;
    [SerializeField] AudioSource sfxSource;

    [Header("------------ Audio Clip ------------")]
    public AudioClip background;
    public AudioClip death;
    public AudioClip finish;
    public AudioClip attack;
    public AudioClip runGrass;

    public static AudioManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        soundSource.clip = background;
        soundSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
