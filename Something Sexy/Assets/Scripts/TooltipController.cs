using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using DG.Tweening;
using DG.Tweening.Core;
using TMPro;
using Image = UnityEngine.UI.Image;

public class TooltipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image tooltip;
    public TextMeshProUGUI tooltipText;
    private Color parchmentColor;
    
    // Start is called before the first frame update
    void Start()
    {
        parchmentColor = new Color(255f, 246f, 225f);
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.DOColor(parchmentColor, .5f);
        tooltipText.DOColor(Color.black, .5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.DOColor(Color.clear, .5f);
        tooltipText.DOColor(Color.clear, .5f);
    }
    
    /*private void OnMouseOver()
    {
        tooltip.DOColor(parchmentColor, .5f);
        tooltipText.DOColor(Color.black, .5f);
    }

    private void OnMouseExit()
    {
        tooltip.DOColor(Color.clear, .5f);
        tooltipText.DOColor(Color.clear, .5f);
    }*/
}
