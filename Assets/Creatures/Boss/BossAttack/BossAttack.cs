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

    AudioSource audioSource;
    [SerializeField] List<AudioClip> soundFX = new List<AudioClip>();
	float secondCounter = 0f;
    float fadeDuration = 1f;

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
    IEnumerator Despawn()
    {

        float elapsedTime = 0.0f;
        Color initialColor = sr.color;



        while (elapsedTime < fadeDuration)
        {
            float normalizedTime = elapsedTime / fadeDuration;
            sr.color = Color.Lerp(initialColor, new Color(initialColor.r, initialColor.g, initialColor.b, 0), normalizedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Make sure the character is fully transparent
        sr.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0);

        // Destroy the character after fading
        Destroy(this.gameObject);
    }
    void DamageAndPush()
    {
        Debug.Log("BOSS DAMAGE");
        audioSource.PlayOneShot(soundFX[Random.Range(0, soundFX.Count)]);
        if (isNear)
        {
            Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
            
            if (GameObject.FindWithTag("Boss"))
			{
                GameObject Boss = GameObject.FindWithTag("Boss");
                Vector3 pushDirection = player.transform.position - Boss.transform.position;
                pushDirection.Normalize();

                player.Push(pushDirection * Random.Range(20, 30));
                Debug.Log("PLAYER CAUGHT IN RADIUS");
            }
            player.TakeDamage(damage);
        }
        StartCoroutine(Despawn());
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
        audioSource = this.GetComponent<AudioSource>();
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
