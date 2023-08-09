using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    #region ExternalLinks

    HealthScript healthScript;

    public GameObject particlesPrefab;
    private GameObject particleObject;

    [SerializeField] List<AudioClip> soundFX;

    #endregion

    #region Attributes

    public int damage = 5;
    IEnumerator LifeSpan(float interval)
	{
        yield return new WaitForSeconds(interval);
        Kill();
    }
    #endregion

    #region Unity
    void Kill()
    {
        particleObject.GetComponent<ParticleEmitter>().Remove();
        Destroy(gameObject); // Destroy the bullet
    }

    void Start()
    {
        StartCoroutine(LifeSpan(3));
        particleObject = Instantiate(particlesPrefab, transform.position, Quaternion.identity);
		GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>().audioManager.PlayAudio(soundFX[Random.Range(0, soundFX.Count)]);
    }
	void Update()
	{
		particleObject.transform.position = this.transform.position;
	}


	private bool ishit = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "Enemy" || collision.collider.tag == "RangedEnemy" || collision.collider.tag == "StrongEnemy" && ishit == false)
		{
            ishit = true;            
            healthScript = collision.gameObject.GetComponent<HealthScript>();
            healthScript.TakeDamage(1, collision.gameObject);
            Kill();
        }
        else
		{
            Kill();
        }

        //check here to see if hitting enemy
    }
    #endregion
}
