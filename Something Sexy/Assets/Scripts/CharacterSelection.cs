using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public static CharacterSelection instance;

    public int playerIndex;
    
    public Image Antony;
    public Image Beatrice;
    public Image Benedick;
    public Image Cleopatra;
    public Image Hamlet;
    public Image Juliet;
    public Image Kate;
    public Image Ophelia;
    public Image Petruchio;
    public Image Romeo;
    
    // Start is called before the first frame update
    void Start()
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
        
        // Fades each sprite from alpha of 0 to alpha 1 (or 255) over a period of 2.5 seconds
        Antony.DOFade(1f, 2.5f);
        Beatrice.DOFade(1f, 2.5f);
        Benedick.DOFade(1f, 2.5f);
        Cleopatra.DOFade(1f, 2.5f);
        Hamlet.DOFade(1f, 2.5f);
        Juliet.DOFade(1f, 2.5f);
        Kate.DOFade(1f, 2.5f);
        Ophelia.DOFade(1f, 2.5f);
        Petruchio.DOFade(1f, 2.5f);
        Romeo.DOFade(1f, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CharacterSelect(int selectionIndex)
    {
        AudioManager.instance.PlaySound(AudioManager.instance.gameSounds[3]); //play click sound
        playerIndex = selectionIndex;
        SceneManager.LoadScene(3);
    }
}
