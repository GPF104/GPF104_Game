using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    [SerializeField] Image faderImage;
    [SerializeField] public float fadeTime = 1f;

	[ContextMenu("FadeOut")]
    public void FadeOut()
	{
        faderImage.CrossFadeAlpha(0, fadeTime, false);
    }

	[ContextMenu("FadeIn")]
    public void FadeIn()
	{
        faderImage.CrossFadeAlpha(1, fadeTime, false);
	}
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
