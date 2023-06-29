using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    GameManager gameManager;

	[SerializeField] public List<GameObject> enemies = new List<GameObject>();


    public void Spawn(float difficulty)
    {
        int enemyIndex = 0;
        int random = Random.Range(0, (int)difficulty);
        Debug.Log("RANDOM " + random + " DIFFICULTY" + difficulty);
        
        if (random >= 85 && random < 95)
		{
            enemyIndex = 1;
		}
        else if (random >= 95)
		{
            enemyIndex = 2;
		}

        GameObject go = Instantiate(enemies[enemyIndex], transform);
        go.transform.SetParent(GameObject.Find("Enemies").transform);
        go.transform.localScale = Vector3.one;
        Debug.Log("Difficulty: " + difficulty + " Spawned enemy at " + go.transform.position);
    }


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Bubble")
		{
            Spawn(gameManager.timeManager.difficulty);
		}
	}
}
