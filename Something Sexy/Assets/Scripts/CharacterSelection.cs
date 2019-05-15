using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public static CharacterSelection instance;

    public int playerIndex;
    
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
