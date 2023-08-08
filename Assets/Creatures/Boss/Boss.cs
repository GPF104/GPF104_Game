using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Boss_HealthUI healthUI;
    GameManager gameManager;
    HealthScript health;

    GameObject blip;
    [SerializeField] public int MAX_HEALTH = 250;
    [SerializeField] public int Health = 250;
    [SerializeField] int Damage = 15;

    IEnumerator Tracker()
	{
        yield return new WaitForSeconds(0.25f);
        if (blip != null)
        {
            GameObject.FindObjectOfType<GameManager>().uiHandler.uiMap.UpdateBlipPosition(blip, this.transform.position);
        }
    }

    IEnumerator Die()
	{
        yield return new WaitForSeconds(0.25f);
        Destroy(this.gameObject);
    }

    public void Defeat()
	{
        StartCoroutine(Die());
	}

    public void TakeDamage(int damage)
    {
        Health += -damage;
        healthUI.SetHealth(Health);
        if (Health <= 0)
		{
            Defeat();
		}
    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        health = this.gameObject.GetComponent<HealthScript>();
        if (GameObject.FindObjectOfType<Boss_HealthUI>())
		{
            healthUI = GameObject.FindObjectOfType<Boss_HealthUI>().GetComponent<Boss_HealthUI>();
		}

        if (blip == null)
        {
            blip = GameObject.FindObjectOfType<GameManager>().uiHandler.uiMap.AddMapElement(BlipType.boss);
        }
        healthUI.SetBoss(this.gameObject);
        StartCoroutine(Tracker());
    }

    void OnDestroy()
    {
        Destroy(blip);
        healthUI.SetHealth(MAX_HEALTH);
        gameManager.bossManager.Defeated();
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Bullet")
		{
            Debug.Log("Boss Hit");
            TakeDamage(collision.gameObject.GetComponent<ProjectileScript>().damage);
		}
	}
}
