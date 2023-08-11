using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    #region ExternalLinks

    HealthScript healthScript;

    public GameObject particlesPrefab;
    private GameObject particleObject;
    [SerializeField] GameObject HitParticles;

    

    #endregion

    #region Attributes

    AudioSource audioSource;
    [SerializeField] List<AudioClip> soundFX;

    public int damage = 5;
    bool destroyed = false;
    IEnumerator LifeSpan(float interval)
	{
        yield return new WaitForSeconds(interval);
        Kill();
    }
    #endregion

    #region Unity
    void Kill()
    {
        destroyed = true;
        if (particleObject != null)
		{
            particleObject.GetComponent<ParticleEmitter>().Remove();
        }
        Destroy(gameObject); // Destroy the bullet
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(LifeSpan(3));
        particleObject = Instantiate(particlesPrefab, transform.position, Quaternion.identity);
        audioSource.PlayOneShot(soundFX[Random.Range(0, soundFX.Count)]);
    }
	void Update()
	{
        if (particleObject != null)
		{
            particleObject.transform.position = this.transform.position;
        }
	}


	private bool ishit = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "Enemy" || collision.collider.tag == "RangedEnemy" || collision.collider.tag == "StrongEnemy" && ishit == false)
		{
            ishit = true;            
            healthScript = collision.gameObject.GetComponent<HealthScript>();
            healthScript.TakeDamage(1, collision.gameObject);
            GameObject gobject = Instantiate(HitParticles);
            gobject.transform.position = this.transform.position;
            Kill();
        }
        else
		{
            GameObject gobject = Instantiate(HitParticles);
            gobject.transform.position = this.transform.position;
            Kill();
        }

        //check here to see if hitting enemy
    }
    #endregion
}
