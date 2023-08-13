using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	#region ExternalLinks

	GameManager gameManager;

    #endregion

    #region Attributes

    [SerializeField] public bool isDev = false;
	[SerializeField] List<AudioClip> damageSFX = new List<AudioClip>();
	[SerializeField] List<AudioClip> potionSFX = new List<AudioClip>();
    [SerializeField] List<AudioClip> scrollSFX = new List<AudioClip>();


    [SerializeField] GameObject healParticles;
    int Health = 100;
    public int scrolls = 0;
    public int potions = 0;
    public int healAmount = 30;
    public bool isPushed = false;

    // Audio
    AudioSource audioSource;
    [SerializeField] AudioClip potionHeal;
    [SerializeField] AudioClip potionPickup;


    [SerializeField] bool godMode = false;

    IEnumerator UpdatePosition()
	{
        yield return new WaitForSeconds(0.5f);
        //playerfacing is direction the player is facing
        gameManager.uiHandler.uiMap.UpdatePositions(this.transform.position, this.GetComponent<Rigidbody2D>().rotation);
        StartCoroutine(UpdatePosition());
    }

    public void TakeDamage(int amount)
	{
        if (!godMode)
		{
            Health = Health - amount;
            gameManager.uiHandler.uiHealth.SetHealth(Health);
            gameManager.mainCamera.DoShake(0.25f);
            gameManager.audioManager.PlayAudio(damageSFX[0]);
            if (Health <= 0)
            {
                gameManager.GameOver();
            }
        }
	}

    public void Heal()
    {
        if (potions > 0 && Health != 100)
		{
            Health = Health + healAmount;
            AddPotion(-1);
            audioSource.PlayOneShot(potionHeal);
            GameObject gob = Instantiate(healParticles);
            gob.transform.position = this.transform.position;

            if (Health >= 100)
            {
                Health = 100;
            }
            gameManager.uiHandler.uiHealth.SetHealth(Health);

            Debug.Log("healing" + Health);
        }
    }

    IEnumerator PushCooldown()
	{
        yield return new WaitForSeconds(0.25f);
        isPushed = false;
    }
    public void Push(Vector2 source)
	{
        // Apply a force to push the player away
        isPushed = true;
        this.GetComponent<Rigidbody2D>().AddForce(source, ForceMode2D.Impulse);
        StartCoroutine(PushCooldown());
    }

    public void AddScroll(int amount)
    {
        scrolls += amount;
        audioSource.PlayOneShot(scrollSFX[Random.Range(0, scrollSFX.Count)]);
        gameManager.uiHandler.scrollCounter.SetScroll(scrolls);
    }
    public void AddPotion(int amount)
	{
        potions += amount;
		audioSource.PlayOneShot(potionSFX[Random.Range(0, potionSFX.Count)]);
        gameManager.uiHandler.potionCounter.SetPotion(potions);
	}

	#endregion

	#region Unity
	// Start is called before the first frame update
	void Start()
    {
        if (!isDev)
		{
            gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
            audioSource = this.GetComponent<AudioSource>();
            StartCoroutine(UpdatePosition());
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "EnemyBullet")
		{
            TakeDamage(other.gameObject.GetComponent<EnemyProjectileScript>().damage);
		}             
    }
	#endregion
}
