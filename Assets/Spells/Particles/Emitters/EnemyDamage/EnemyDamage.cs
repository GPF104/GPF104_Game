using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    ParticleSystem m_ParticleSystem;
    IEnumerator LifeSpan()
	{
        yield return new WaitForSeconds(m_ParticleSystem.main.duration);
        Destroy(this.gameObject);
    }

    void Start()
    {
        m_ParticleSystem = GetComponent<ParticleSystem>();
        // Destroy enemy defeat particles once they've played.
        StartCoroutine(LifeSpan());
    }
}
