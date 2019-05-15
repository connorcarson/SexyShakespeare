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
    public AudioClip[] audienceReactions;
    
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
        
        audioSource = GetComponent<AudioSource>(); //get the audio source to be used for everything other than the theme music
        themeAudioSource = transform.GetChild(0).GetComponent<AudioSource>(); //get the audio source from the child GameObject, which will be used for the theme music

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
        audioSource.PlayOneShot(audioSource.clip);
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
