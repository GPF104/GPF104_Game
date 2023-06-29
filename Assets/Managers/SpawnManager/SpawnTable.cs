using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyEntry
{
	public GameObject referenceObject;
	public int SpawnChance;

	public EnemyEntry(GameObject inputobject, int chance)
	{

	}
}
public class SpawnTable : MonoBehaviour
{

	public List<EnemyEntry> enemies = new List<EnemyEntry>();
	[SerializeField] List<GameObject> enemyObjects;
	[SerializeField] List<float> spawnChance;
	void OnValidate()
	{
		Debug.Log("spawntable changed");
		spawnChance = new List<float>();
		for (int i = 0; i < enemyObjects.Count; i++)
		{
			if (spawnChance.Count != enemyObjects.Count)
			{
				spawnChance.Add(enemyObjects.Count / 100);
			}
		}
	}
}
