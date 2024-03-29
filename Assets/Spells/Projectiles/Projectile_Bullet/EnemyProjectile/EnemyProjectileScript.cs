using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileScript : MonoBehaviour
{
    #region ExternalLinks

    HealthScript healthScript;

    #endregion

    #region Attributes

    public int damage = 5;

    IEnumerator LifeSpan(float interval)
    {
        yield return new WaitForSeconds(interval);
        Destroy(this.gameObject);
    }
    #endregion

    #region Unity

    void Start()
    {
        StartCoroutine(LifeSpan(3));
    }

    private bool ishit = false;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && ishit == false)
        {
            ishit = true;
            Destroy(this.gameObject);
        }
        if (collision.collider.tag == "World")
		{
            Destroy(this.gameObject);
        }
    }
    
    #endregion
}
