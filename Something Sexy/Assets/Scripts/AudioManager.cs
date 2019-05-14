using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Plugins.Core.PathCore;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource audioSource;
    private AudioSource themeAudioSource;
    public AudioClip[] gameSounds;
    
    // Start is called before the first frame update
    void Awake()
    {
        
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        audioSource = GetComponent<AudioSource>();
        themeAudioSource = GetComponentInChildren<AudioSource>();
        
    PlayThemeMusic(gameSounds[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySound(AudioClip clip)
    {
        //Set the clip of the audio source to the clip passed in as a parameter.
        audioSource.clip = clip;
            
        //Play the clip.
        audioSource.Play ();
    }

    public void PlayThemeMusic(AudioClip themeClip)
    {
        //Set the clip of the audio source to the clip passed in as a parameter.
        themeAudioSource.clip = themeClip;

        //Play the clip.
        themeAudioSource.Play();
        themeAudioSource.loop = true;

    }
}
