using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Boss_HealthUI : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    Image hpImage;
    GameObject BossObject;
    int currentHealth = 100;

    public void SetHealth(int health)
	{
        currentHealth = health;
        hpImage.fillAmount = (float)currentHealth / BossObject.GetComponent<Boss>().MAX_HEALTH;
	}

    public void SetBoss(GameObject gobject)
	{
        if (gobject != null)
		{
            BossObject = gobject;
            currentHealth = BossObject.GetComponent<Boss>().Health;
		}
	}

	void Start()
	{
        hpImage = GameObject.Find("HP").GetComponent<Image>();
	}
}
