using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core;

public class YorickMainScene : MonoBehaviour
{
    public Text transitionalWriting;
    public Image textBox;
    public Image textBoxTail;
    public string[] transitionalText;

    private int indexLocation;
    
    // Start is called before the first frame update
    void Start()
    {
        indexLocation = 0;
        transitionalWriting.text = transitionalText[0];

        textBox.DOColor(Color.white, .5f);
        textBoxTail.DOColor(Color.white, .5f);
        transitionalWriting.DOColor(Color.black, .5f);
    }

    public void CallNewLine()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
