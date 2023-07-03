using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Score : MonoBehaviour
{

    private TMP_Text ScoreText;

    public void SetText(string input)
    {
        ScoreText.text = "Score: " + input;
    }

    // Start is called before the first frame update
    void Start()
    {
        ScoreText = this.gameObject.GetComponentInChildren<TMP_Text>();
        SetText(0.ToString());
    }
}
