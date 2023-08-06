using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Boss_HealthUI healthUI;
    GameManager gameManager;
    HealthScript health;

    IEnumerator Tracker()
	{
        yield return new WaitForSeconds(0.25f);
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
    }


}
