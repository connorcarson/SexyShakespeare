using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestionMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.color = Color.black;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("You touched me!");
        text.color = Color.white;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = Color.black;
        
    }
}
