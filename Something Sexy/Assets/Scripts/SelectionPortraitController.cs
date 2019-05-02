using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SelectionPortraitController : MonoBehaviour
{
    public SpriteRenderer Antony;
    public SpriteRenderer Beatrice;
    public SpriteRenderer Benedick;
    public SpriteRenderer Cleopatra;
    public SpriteRenderer Hamlet;
    public SpriteRenderer Juliet;
    public SpriteRenderer Kate;
    public SpriteRenderer Ophelia;
    public SpriteRenderer Petruchio;
    public SpriteRenderer Romeo;
    
    // Start is called before the first frame update
    void Start()
    {
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
}
