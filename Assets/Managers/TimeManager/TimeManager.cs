using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
	#region ExternalLinks

	LevelGenerator levelGenerator;
	GameManager gameManager;
	UIHandler uiHandler;

	GameObject towerTop;
	#endregion

	#region Attributes

	[SerializeField] GameObject spawner;
	[SerializeField] GameObject scrolls;
	[SerializeField] GameObject potions;
	//	Variables
	private bool active = true;
	private IEnumerator timer;
	public float difficulty = 0;
	private float secondCount, minuteCount;
	private float a = 0.00f;
	private float b = 0.1f;
	private float c = 1.0f;

	//	Spawn Timers
	[SerializeField] float BubbleInterval = 2;
	[SerializeField] float SpawnerInterval = 30;
	[SerializeField] int ScrollChance = 5;
	[SerializeField] int PotionChance = 13;
	[SerializeField] int bossSpawnScoreThreshold = 450;
	[SerializeField] int bossSpawnTimeThreshold = 240;

	public LayerMask worldLayerMask; // Assign the 'world' layer to this in the Inspector
	public float minDistanceFromObjects = 3f;
	public float minDistanceFromPlayer = 10f;
	public float maxDistanceFromPlayer = 15f;


	int bossCounter = 0;
	[ContextMenu("AddTime")]
	void AddTime()
	{
		secondCount = 120;
		difficulty = SetDifficulty();
	}

	[ContextMenu("AddSpawner")]
	void AddPortal()
	{
		StartCoroutine(DifficultyScale());
	}
	float SetDifficulty()
	{
		return 10 * (a * Mathf.Pow(secondCount, 2) + b * secondCount + c);
	}

	[ContextMenu("AddScore")]
	void AddScore()
	{
		gameManager.score += 1000;
	}
	// Boss Trackers
	[ContextMenu("SpawnBubble")]
	void SpawnBubble()
	{
		Debug.Log("Spawned Bubbles");
		StartCoroutine(Bubble(0.1f));
	}
	//	Methods
	IEnumerator Timer(float interval)
	{
		//To-do spawn queue
		while (active)
		{
			yield return new WaitForSeconds(interval);
			secondCount += 1;
			minuteCount = (int)Mathf.Floor(secondCount / 60);
			difficulty = 10 * (a * Mathf.Pow(secondCount, 2) + b * secondCount + c);
			gameManager.uiHandler.uiTimer.SetText(GetTimeString(minuteCount, secondCount));
			int scrollRoll = Random.Range(1, 100);
			int potionRoll = Random.Range(1, 100);

			if (secondCount % BubbleInterval == 0)
			{
				Debug.Log("Bubble Spawned: " + difficulty);
				for (int i = 0; i < Random.Range(1, Mathf.Floor(difficulty*0.5f)); i++)
				{
					yield return new WaitForSeconds(0.5f);
					StartCoroutine(Bubble(2));
				}
			}
			if (scrollRoll > (100-ScrollChance))
			{
				Debug.Log("SCROLL ROLL:" + scrollRoll + " " + (100 - ScrollChance));
				StartCoroutine(Scroll());
			}
            if (potionRoll > (100 - PotionChance))
            {
				Debug.Log("Potion ROLL:" + potionRoll + " " + (100 - PotionChance));
				StartCoroutine(Potion());
            }

			if (secondCount % SpawnerInterval == 0)
				AddSpawner();

			if (gameManager.score > 0 && gameManager.score > (bossSpawnScoreThreshold))
			{

				if (!GameObject.FindGameObjectWithTag("Boss"))
				{
					gameManager.bossManager.Initialize();
					bossCounter++;
					bossSpawnScoreThreshold = bossSpawnScoreThreshold * (bossCounter + 2);
					Debug.LogWarning("Next Boss Score Threshold: " + bossSpawnScoreThreshold);
				}
			}

			yield return Timer(interval);
			Timer(interval);
		}
	}

	void AddSpawner()
	{
		levelGenerator.AddSpawner(spawner);
		Debug.Log(string.Format("Difficulty: {0} Spawner spawned. There are {1} active spawners.", difficulty, levelGenerator.spawners.Count));
	}
	IEnumerator DifficultyScale()
	{
		levelGenerator.AddSpawner(spawner);
		Debug.Log(string.Format("Difficulty: {0} Spawner spawned. There are {1} active spawners.", difficulty, levelGenerator.spawners.Count));

		yield return new WaitUntil(() => secondCount % 30 == 0);
	}

	IEnumerator Scroll()
	{
		yield return new WaitForSeconds(0.25f);

		Vector2 randomVector = Random.insideUnitCircle.normalized * Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);

		Vector2 spawnPosition = (Vector2)gameManager.player.transform.position + randomVector;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, minDistanceFromObjects, worldLayerMask);

		if (colliders.Length > 0)
		{
			Debug.LogWarning("Potion overlapped with world, try again");
			StartCoroutine(Potion()); // Retry the potion spawn if it overlaps with objects in the 'world' layer
			yield break;
		}

		GameObject potion = Instantiate(scrolls);
		potion.transform.position = spawnPosition;
		Debug.Log("Scroll spawned at: " + potion.transform.position);
	}

	IEnumerator Potion()
	{
		yield return new WaitForSeconds(0.25f);

		Vector2 randomVector = Random.insideUnitCircle.normalized * Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);

		Vector2 spawnPosition = (Vector2)gameManager.player.transform.position + randomVector;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, minDistanceFromObjects, worldLayerMask);

		if (colliders.Length > 0)
		{
			Debug.LogWarning("Potion overlapped with world, try again");
			StartCoroutine(Potion()); // Retry the potion spawn if it overlaps with objects in the 'world' layer
			yield break;
		}

		GameObject potion = Instantiate(potions);
		potion.transform.position = spawnPosition;
		Debug.Log("Potion spawned at: " + potion.transform.position);
	}

	[ContextMenu("SpawnPotion")]
	void SpawnPotion()
	{
		StartCoroutine(Potion());
	}
	[ContextMenu("SpawnScroll")]
	void SpawnScroll()
	{
		StartCoroutine(Scroll());
	}
	IEnumerator Bubble(float delay)
	{
		yield return new WaitForSeconds(Random.Range(difficulty * 0.5f, 10 / difficulty));

		int randomSpawner = Random.Range(0, levelGenerator.spawners.Count);
		Vector3 pos = towerTop.transform.position;
		GameObject bubble = Instantiate(gameManager.bubble, towerTop.transform.position, Quaternion.identity);

		yield return new WaitForSeconds(delay);
		GameObject goTo = levelGenerator.RandomSpawner();
		bubble.GetComponent<Bubble>().SetTarget(goTo.transform.position, 2);
	}


	public void StartTimer(float interval)
	{
		secondCount = 0;
		minuteCount = 0;
		timer = Timer(interval);
		StartCoroutine(timer);
		StartCoroutine(DifficultyScale());
		
	}
	public void StopTimer()
	{
		active = false;
		StopCoroutine(timer);
	}

	public string GetTimeString(float minutes, float seconds)
	{
		//Return time string
		return minutes.ToString("0#") + ":" + (seconds % 60).ToString("0#");
	}

	public string CurrentTime()
	{
		return GetTimeString(minuteCount, secondCount);
	}
	public float GetTime()
	{
		return (minuteCount*60) + secondCount;
	}
	#endregion

	#region Unity

	// Start is called before the first frame update
	void Start()
    {
		gameManager = GetComponentInParent<GameManager>();
		levelGenerator = GameObject.FindObjectOfType<LevelGenerator>().GetComponent<LevelGenerator>();
		towerTop = GameObject.Find("LightningRod");
    }
	#endregion
}
