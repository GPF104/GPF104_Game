using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollCounter : MonoBehaviour
{
    public static ScrollCounter instance;

    public TMP_Text scrollText;
    public int currentScrolls = 0;

    void Awake()
    {
        instance= this;
    }

    // Start is called before the first frame update
    void Start()
    {
        scrollText.text = "SCROLLS: " + currentScrolls.ToString();
    }

    // Update is called once per frame
    public void IncreaseScrolls(int v)
    {
        currentScrolls += v;
        scrollText.text = "SCROLLS: " + currentScrolls.ToString();
    }
}
