using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{

    SpriteRenderer sr;
	bool isNear = false;
    GameObject Player;
	[SerializeField] float duration = 1.5f;
    [SerializeField] float delay = 0.75f;
    [SerializeField] int damage = 25;
	float secondCounter = 0f;

	Color endColor = Color.red;

    void Damage()
	{
        if (isNear)
		{
            Player.GetComponent<Player>().TakeDamage(damage);
            Debug.Log("PLAYER CAUGHT IN RADIUS");
        }
        Destroy(this.gameObject);
	}

    private IEnumerator WindUp()
    {
        Color startColor = sr.color;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            secondCounter += Time.deltaTime;
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / duration;
            sr.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }
        Damage();
    }
    IEnumerator Delay()
	{
        yield return new WaitForSeconds(delay);
        StartCoroutine(WindUp());
    }
    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        Player = GameObject.FindWithTag("Player");
		StartCoroutine(Delay());
    }

	void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			isNear = true;
		}
	}
	void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			isNear = false;
		}
	}
}
