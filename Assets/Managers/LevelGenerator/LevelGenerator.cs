using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
	[SerializeField] float radius = 20;
    [SerializeField] int MIN_DENSITY = 100;
    [SerializeField] int MAX_DENSITY = 1000;
	[SerializeField] List<GameObject> Flora = new List<GameObject>();
    [SerializeField] List<GameObject> Rocks = new List<GameObject>();
    [SerializeField] List<GameObject> Props = new List<GameObject>();
    [SerializeField] List<GameObject> Decals = new List<GameObject>();

    IEnumerator SlowGenerate(List<GameObject> gameObjects)
	{
        for (int i = 0; i < gameObjects.Count; i++)
        {
            for (int j = 0; j < Random.Range(MIN_DENSITY, MAX_DENSITY); j++)
            {
                Vector2 pos = Random.insideUnitSphere * radius;
                GameObject go = Instantiate(gameObjects[Random.Range(0, gameObjects.Count)]);
                go.transform.position = pos;
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
    void Generate(List<GameObject> gameObjects)
	{
        for (int i = 0; i < gameObjects.Count; i++)
		{
            for (int j = 0; j < Random.Range(MIN_DENSITY, MAX_DENSITY); j++)
			{
                Vector2 pos = Random.insideUnitSphere * radius;
                GameObject go = Instantiate(gameObjects[Random.Range(0, gameObjects.Count)]);
                go.transform.position = pos;
            }
		}
	}

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SlowGenerate(Flora));
        //Generate(Flora);
        //Generate(Rocks);
        //Generate(Props);
        //Generate(Decals);
    }

}
