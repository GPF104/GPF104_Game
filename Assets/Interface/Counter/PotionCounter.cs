using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PotionCounter : MonoBehaviour
{

    private TMP_Text potionText;
    public int currentPotions = 0;

    // Start is called before the first frame update
    void Start()
    {
        potionText = gameObject.GetComponentInChildren<TMP_Text>();
        potionText.text = ": " + currentPotions.ToString();
    }

    // Update is called once per frame
    public void IncreaseScrolls(int v)
    {
        currentPotions += v;
        potionText.text = ": " + currentPotions.ToString();
    }
    public void SetPotion(int potionNumber)
    {
        potionText.text = ": " + potionNumber.ToString();
    }

}
