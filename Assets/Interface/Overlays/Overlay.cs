using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overlay : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject overlay;
    RectTransform overlayScale;
    SpriteRenderer overlayImage;
    float opacity = 0;
    Vector2 ScreenResolution = new Vector2(Screen.width, Screen.height);

    public void SetOpacity(float transparency)
	{
        opacity = transparency;
        overlayImage.color = new Color(0, 0, 0, transparency);
	}
     public float GetOpacity()
	{
        return opacity;
	}
    void Start()
    {
        overlayScale = GameObject.FindGameObjectWithTag("Overlay").GetComponent<RectTransform>();
        overlayScale.localScale = ScreenResolution;
        overlayScale.transform.position = Camera.main.WorldToViewportPoint(ScreenResolution);

        overlayImage = GameObject.FindGameObjectWithTag("Overlay").GetComponent<SpriteRenderer>();
        overlayImage.color = new Color(0, 0, 0, opacity);
    }

}
