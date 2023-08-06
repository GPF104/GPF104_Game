using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	#region ExternalLinks

	GameManager gameManager;

	[SerializeField] public List<GameObject> enemies = new List<GameObject>();
	[SerializeField] int collisionDamage = 2;
	#endregion

	#region Attributes

	public void Spawn(float difficulty)
    {
        int enemyIndex = 0;
        int random = Random.Range(0, (int)difficulty);
        
        if (random >= 85 && random < 120)
		{
            enemyIndex = 1;
		}
        else if (random >= 120)
		{
            enemyIndex = 2;
		}
        float randomScale = Random.Range(1, 1.5f);
        GameObject go = Instantiate(enemies[enemyIndex], transform);
        go.transform.SetParent(GameObject.Find("Enemies").transform);
        go.transform.localScale = Vector3.one * randomScale;
        Debug.Log("Difficulty: " + difficulty + " Spawned enemy at " + go.transform.position);
    }
	#endregion

	#region Unity
	// Start is called before the first frame update
	void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Bubble")
		{
            Spawn(gameManager.timeManager.difficulty);
		}
		if (collision.tag == "Player")
		{
			collision.gameObject.GetComponent<Player>().TakeDamage(collisionDamage);
		}
	}
	#endregion
}
