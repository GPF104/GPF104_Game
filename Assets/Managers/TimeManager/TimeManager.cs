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
	[SerializeField] float SpawnerInterval = 15;
	[SerializeField] int ScrollChance = 5;
	[SerializeField] int PotionChance = 10;
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
				for (int i = 0; i < levelGenerator.spawners.Count; i++)
				{
					yield return new WaitForSeconds(0.5f);
					StartCoroutine(Bubble(2));
				}
			}
			Debug.Log("SCROLL ROLL:" + scrollRoll + " " + (100 - ScrollChance));
			if (scrollRoll > (100-ScrollChance))
			{
				
				StartCoroutine(Scroll());
			}
            if (potionRoll > (100 - PotionChance))
            {

                StartCoroutine(Potion());
            }

            if (secondCount % SpawnerInterval == 0)
				StartCoroutine(DifficultyScale());

			yield return Timer(interval);
			Timer(interval);
		}
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
		
		GameObject scroll = Instantiate(scrolls);
		Vector2 randomVector = Random.insideUnitCircle.normalized * 3;

		scroll.transform.position = gameManager.player.transform.position + (Vector3)randomVector;
		Debug.Log("Scroll spawned at: " + scroll.transform.position);
	}
    IEnumerator Potion()
    {
        yield return new WaitForSeconds(0.25f);

        GameObject potion = Instantiate(potions);
        Vector2 randomVector = Random.insideUnitCircle.normalized * 3;

        potion.transform.position = gameManager.player.transform.position + (Vector3)randomVector;
        Debug.Log("Scroll spawned at: " + potion.transform.position);
    }
    IEnumerator Bubble(float delay)
	{
		yield return new WaitForSeconds(Random.Range(difficulty * 0.5f, 10 / difficulty));

		int randomSpawner = Random.Range(0, levelGenerator.spawners.Count);
		Vector3 pos = towerTop.transform.position;
		GameObject bubble = Instantiate(gameManager.bubble, towerTop.transform.position, Quaternion.identity);

		yield return new WaitForSeconds(delay);

		bubble.GetComponent<Bubble>().SetTarget(levelGenerator.spawners[randomSpawner].transform.position, 2);
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
