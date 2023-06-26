using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Map : MonoBehaviour
{

    RectTransform rectTransform;
    UIHandler uiHandler;


    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GameObject.Find("MapBG").GetComponent<RectTransform>();
        uiHandler = GameObject.FindObjectOfType<UIHandler>().GetComponent<UIHandler>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
