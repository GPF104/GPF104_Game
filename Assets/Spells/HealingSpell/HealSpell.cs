using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSpell : MonoBehaviour
{
    [SerializeField] GameObject Player;
    bool healed = false;
    public int heal = 20;
    void Start()
    {
        
    }

    public void healing()
    {
        if (healed = true)
        {
            //we heal the player
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
