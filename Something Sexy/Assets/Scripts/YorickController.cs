﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

public class YorickController : MonoBehaviour
{

    public TextMeshProUGUI InterfaceText;

    public Button nextButton;

    public string[] messageText;

    private int indexLocation;
    
    // Start is called before the first frame update
    void Start()
    {
        indexLocation = 0;
        InterfaceText.text = messageText[0];
        indexLocation++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallNewLine()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.gameSounds[3]); //play click sound 
        //Debug.Log("Index Location = " + indexLocation);
        //InterfaceText.DOColor(Color.clear, 1f);
       // InterfaceText.DOColor(Color.black, 1f).SetDelay(1f);
        
        if (indexLocation < messageText.Length)
        {
            InterfaceText.text = messageText[indexLocation];
            indexLocation++;

        }

        else
        {
            SceneManager.LoadScene("Character_Selection_Scene");
        }

    }
}
