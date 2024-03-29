using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;
    public int scoreValue = 5;
    SpriteRenderer sr;
    Collider2D col;
    [SerializeField] AudioClip damageSFX;
    [SerializeField] GameObject damageParticles;

    [SerializeField] float fadeDuration = 0.5f;
    AudioSource audioSource;

    public bool isAlive = true;


    [SerializeField] List<GameObject> Drops = new List<GameObject>();
    [SerializeField] int spawnChance = 20;

    bool WillDrop()
    {
        int RandomRoll = Random.Range(1, 100);
        Debug.Log("Random Roll: " + RandomRoll);
        if (RandomRoll > (100-spawnChance))
		{
            return true;
		}
        return false;
    }

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        currentHealth = maxHealth;
        sr = this.GetComponent<SpriteRenderer>();
        col = this.GetComponent<Collider2D>();
        if (damageSFX == null)
		{
            Debug.LogError(this.gameObject.name + " has no damageSFX");
		}
    }
    IEnumerator Despawn(GameObject character)
    {
        isAlive = false;

        float elapsedTime = 0.0f;
        Color initialColor = sr.color;

        // Disable the collider
        col.enabled = false;

        while (elapsedTime < damageSFX.length+1)
        {
            float normalizedTime = elapsedTime / fadeDuration;
            sr.color = Color.Lerp(initialColor, new Color(initialColor.r, initialColor.g, initialColor.b, 0), normalizedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Make sure the character is fully transparent
        sr.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0);
        if (WillDrop())
		{
            GameObject go = Instantiate(Drops[Random.Range(0, Drops.Count)]);
            go.transform.position = this.transform.position;
		}
        // Destroy the character after fading
        Destroy(character);
    }
    public void die(GameObject character)
    {
        if (this.gameObject.tag == "Boss")
		{
            this.gameObject.GetComponent<Boss>().Defeat();
		}
        else
		{
            GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>().AddScore(scoreValue);
            audioSource.PlayOneShot(damageSFX);
            StartCoroutine(Despawn(character));
            //Destroy(character);
        }
    }
    public void TakeDamage(int amount, GameObject character)
    {
        currentHealth -= amount;
        Instantiate(damageParticles, this.transform.position, Quaternion.identity);

        if (this.gameObject.tag == "Boss")
		{
            this.gameObject.GetComponent<Boss>().TakeDamage(amount);
        }
        if(currentHealth <= 0)
        {
            die(character);
        }
    }
}
