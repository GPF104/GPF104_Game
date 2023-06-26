using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] public List<GameObject> enemies = new List<GameObject>();

    public void Spawn(int difficulty)
	{
        
        GameObject go = Instantiate(enemies[Random.Range(0, enemies.Count)], this.transform);
        go.transform.parent.SetParent(GameObject.Find("Enemies").transform);
        Debug.Log("Difficulty: " + difficulty + " Spawned enemy at " + go.transform.position);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Bubble")
		{
            Spawn(1);
		}
	}
}
