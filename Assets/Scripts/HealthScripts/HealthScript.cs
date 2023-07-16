using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;
    public int scoreValue = 5;
    [SerializeField] AudioClip damageSFX;
    [SerializeField] GameObject damageParticles;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void die(GameObject character)
    {
        GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>().AddScore(scoreValue);
        GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>().audioManager.PlayAudio(damageSFX);
        Destroy(character);
    }
    public void TakeDamage(int amount, GameObject character)
    {
        currentHealth -= amount;
        Debug.Log(amount);
        Instantiate(damageParticles, this.transform.position, Quaternion.identity);

        if(currentHealth <= 0)
        {
            die(character);
        }
    }
}
