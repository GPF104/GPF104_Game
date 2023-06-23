using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overlay : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject overlay;
    GameObject overlayImage;

    Vector2 ScreenResolution = new Vector2(Screen.width, Screen.height);

    void Start()
    {
        overlayImage = GameObject.Find("Overlay");
        overlayImage.transform.localScale = ScreenResolution;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
