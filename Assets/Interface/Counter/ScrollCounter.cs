using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollCounter : MonoBehaviour
{
    private TMP_Text scrollText;
    public int currentScrolls = 0;

    // Start is called before the first frame update
    void Start()
    {
        scrollText = gameObject.GetComponentInChildren<TMP_Text>();
        scrollText.text = ": " + currentScrolls.ToString();
    }

    // Update is called once per frame
    public void IncreaseScrolls(int v)
    {
        currentScrolls += v;
        scrollText.text = ": " + currentScrolls.ToString();
    }
    public void SetScroll(int scrollNumber)
    {
        scrollText.text = ": " + scrollNumber.ToString();
    }
}
