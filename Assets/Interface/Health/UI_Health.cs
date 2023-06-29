using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Health : MonoBehaviour
{

    [SerializeField] GameObject HPBar;
    Image HP;
    void SetHealth(int health)
	{
        HP.fillAmount = health;
	}
    // Start is called before the first frame update
    void Start()
    {
        HP = HPBar.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
