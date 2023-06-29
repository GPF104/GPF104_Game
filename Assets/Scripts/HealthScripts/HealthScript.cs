using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void die(GameObject character)
    {
        Destroy(character);
    }
    public void TakeDamage(int amount, GameObject character)
    {
        currentHealth -= amount;
        Debug.Log(amount);

        if(currentHealth <= 0)
        {
            die(character);
        }
    }
}
