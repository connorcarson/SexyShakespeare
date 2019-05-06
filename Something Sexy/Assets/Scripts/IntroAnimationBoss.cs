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
    public GameObject leftCurtain;
    public GameObject rightCurtain;
    public GameObject centerCurtain;
    public GameObject button;
    private AudioSource audioSource;
    private AudioClip thudSound;
    public TextMeshProUGUI title;

    private float seconds;
    
    // Start is called before the first frame update
    void Start()
    {
        title.color = Color.clear;
        audioSource = GetComponent<AudioSource>();
        thudSound = (AudioClip)Resources.Load("thud sound");
        audioSource.clip = thudSound;
        leftCurtain.transform.DOMove(new Vector3(-7.85f, -1.25f, 0f), .5f);
        StartCoroutine(DelayedSound(.3f));
        rightCurtain.transform.DOMove(new Vector3(7.62f, -1.25f, 0f), .5f).SetDelay(1f);
        StartCoroutine(DelayedSound(1.3f));
        centerCurtain.transform.DOMove(new Vector3(0f, 4.25f, 0f), .5f).SetDelay(2f);
        StartCoroutine(DelayedSound(2.2f));
        title.DOColor(Color.black, 1.5f).SetDelay(2.8f);
        StartCoroutine(DelayedButton(4.3f));
    }

    IEnumerator DelayedSound(float seconds)
    {
        yield return new WaitForSeconds (seconds);
        audioSource.Play();
             
    }

    IEnumerator DelayedButton(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        button.SetActive(true);
    }

    public void LoadNewScene()
    {
        SceneManager.LoadScene(1);
    }
}
