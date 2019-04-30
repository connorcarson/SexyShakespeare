using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITestingScript : MonoBehaviour
{
    public GameObject textObject;
    
    // Start is called before the first frame update
    void Start()
    {
        textObject.GetComponent<PanelMessageBox>().messageList[0] = "hello world!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
