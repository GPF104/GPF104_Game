using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    // Start is called before the first frame update
    ParticleSystem m_ParticleSystem;
    IEnumerator LifeSpan()
	{
        yield return new WaitForSeconds(m_ParticleSystem.main.duration);
        Destroy(this);
    }
    void Start()
    {
        m_ParticleSystem = GetComponent<ParticleSystem>();
    }


}
