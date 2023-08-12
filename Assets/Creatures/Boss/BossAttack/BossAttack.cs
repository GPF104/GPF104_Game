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

	Color endColor = new Color(0,0,0,1);

    float CalculatePushForce(float distance, float maxDistance)
    {
        // Use a custom function to calculate push force based on distance
        // You can adjust this function to achieve the desired behavior
        float minForce = 5f; // Minimum push force
        float maxForce = 10f; // Maximum push force

        // Linearly interpolate the force based on distance
        float t = Mathf.Clamp01(distance / maxDistance);
        float pushForce = Mathf.Lerp(maxForce, minForce, t);

        return pushForce;
    }

    void DamageAndPush()
    {
        if (isNear)
        {
            Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
            GameObject Boss = GameObject.FindWithTag("Boss");
            Vector3 pushDirection = player.transform.position - Boss.transform.position;
            pushDirection.Normalize();

            player.Push(pushDirection * Random.Range(20,30));
            player.TakeDamage(damage);

            Debug.Log("PLAYER CAUGHT IN RADIUS");
        }

        Destroy(gameObject);
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
        DamageAndPush();
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
