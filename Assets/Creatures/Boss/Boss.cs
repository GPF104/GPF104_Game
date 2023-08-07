using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Boss_HealthUI healthUI;
    GameManager gameManager;
    HealthScript health;

    GameObject blip;

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
    }

    public void Defeat()
	{
        StartCoroutine(Die());
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

        StartCoroutine(Tracker());
    }

    public void TakeDamage(int damage)
	{

	}
    void OnDestroy()
    {
        Destroy(blip);
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Projectile")
		{
            Debug.Log("Boss Hit");
		}
	}
}
