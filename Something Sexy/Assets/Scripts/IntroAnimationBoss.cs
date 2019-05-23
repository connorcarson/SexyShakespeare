using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroAnimationBoss : MonoBehaviour
{
    public Image leftCurtain;
    public Image rightCurtain;
    public Image centerCurtain;
    public GameObject button;
    public TextMeshProUGUI title;

    public Image Antony;
    public Image Cleopatra;
    public Image Beatrice;
    public Image Benedick;
    public Image Hamlet;
    public Image Ophelia;
    public Image Juliet;
    public Image Romeo;
    public Image Petruchio;
    public Image Kate;
    
    private float seconds;
    
    // Start is called before the first frame update
    void Start()
    {
        title.color = Color.clear;

        leftCurtain.rectTransform.DOAnchorPos(new Vector2(-383.46f, -45.368f), .5f );
        rightCurtain.rectTransform.DOAnchorPos(new Vector2(384f, -45.368f), .5f).SetDelay(1f);
        centerCurtain.rectTransform.DOAnchorPos(new Vector2(-6.150024f, 211f), .5f).SetDelay(2f);
        title.DOColor(Color.white, 1.5f).SetDelay(2.8f);
        Antony.DOColor(Color.white, .5f).SetDelay(2.8f);
        Cleopatra.DOColor(Color.white, .5f).SetDelay(3.1f);
        Juliet.DOColor(Color.white, .5f).SetDelay(3.4f);
        Hamlet.DOColor(Color.white, .5f).SetDelay(3.7f);
        Romeo.DOColor(Color.white, .5f).SetDelay(4f);
        Ophelia.DOColor(Color.white, .5f).SetDelay(4.3f);
        Benedick.DOColor(Color.white, .5f).SetDelay(4.6f);
        Beatrice.DOColor(Color.white, .5f).SetDelay(4.9f);
        Petruchio.DOColor(Color.white, .5f).SetDelay(5.2f);
        Kate.DOColor(Color.white, .5f).SetDelay(5.5f);
        StartCoroutine(DelayedButton(5.5f));
    }

    IEnumerator DelayedButton(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        button.SetActive(true);
    }

    public void LoadNewScene()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.gameSounds[3]); //play selection sound
        SceneManager.LoadScene(1);
    }
}
