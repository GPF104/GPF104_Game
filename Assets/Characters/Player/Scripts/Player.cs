using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameManager gameManager;
	[SerializeField] List<AudioClip> damageSFX = new List<AudioClip>();

    int Health = 100;

    IEnumerator UpdatePosition()
	{
        yield return new WaitForSeconds(0.5f);
        gameManager.uiHandler.uiMap.UpdatePositions(this.transform.position);
        StartCoroutine(UpdatePosition());
    }


    public void TakeDamage(int amount)
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
    public int scrolls = 0;

    public void AddScroll(int amount)
    {
        scrolls += amount;
        Debug.Log("Logging: " + scrolls);
        gameManager.uiHandler.scrollCounter.SetScroll(scrolls);
    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
        StartCoroutine(UpdatePosition());
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "EnemyBullet")
		{
            TakeDamage(other.gameObject.GetComponent<EnemyProjectileScript>().damage);
		}             
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Bindi")
		{
            Debug.Log("DAMAGE " + collision.gameObject.GetComponent<BindiScript>().damage);
            TakeDamage(collision.gameObject.GetComponent<BindiScript>().damage);
        }
	}
}
