using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Boss_HealthUI : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] Image hpImage;
    GameObject BossObject;
    int MaxHealth = 100;
    int currentHealth = 100;

    public void SetHealth(int health)
	{
        currentHealth = health;
        hpImage.fillAmount = currentHealth / MaxHealth;
	}

    public void SetBoss(GameObject gobject)
	{
        if (gobject != null)
		{
            BossObject = gobject;
            MaxHealth = BossObject.GetComponent<HealthScript>().maxHealth;
            currentHealth = BossObject.GetComponent<HealthScript>().currentHealth;
		}
	}
}
