using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LevelGenerator : MonoBehaviour
{

    enum GenerateType {
        Flora,
        Rocks,
        Props,
        Decals
    }

    #region ExternalLinks

    AutoTile autoTile;
    Boundary boundary;
    GameManager gameManager;

    #endregion

    #region Attributes

    //  Edit in Unity Editor


    [SerializeField] GameObject Spawner;
    public List<GameObject> spawners = new List<GameObject>();

    [SerializeField] float radius = 20;
    [SerializeField] int MIN_DENSITY = 100;
    [SerializeField] int MAX_DENSITY = 1000;
    [SerializeField] List<GameObject> Flora = new List<GameObject>();
    [SerializeField] List<GameObject> Rocks = new List<GameObject>();
    [SerializeField] List<GameObject> Props = new List<GameObject>();
    [SerializeField] List<GameObject> Decals = new List<GameObject>();

    public Vector3Int levelSize = new Vector3Int(120, 120, 0);

    #endregion

    #region Generation
    //  Slower generation - Might need this for when our objects get more complex.
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
    void Generate(List<GameObject> gameObjects, GenerateType type)
	{
        if (type == GenerateType.Flora)
		{
            for (int i = 0; i < gameObjects.Count; i++)
            {
                for (int j = 0; j < Random.Range(MIN_DENSITY, MAX_DENSITY); j++)
                {
                    Vector2 pos = Random.insideUnitSphere * radius;
                    if (autoTile.GetTile(pos, AutoTile.TileTypes.grass))
					{
                        GameObject go = Instantiate(gameObjects[Random.Range(0, gameObjects.Count)]);
                        go.transform.SetParent(GameObject.Find("Flora").transform);
                        go.transform.position = pos;
                    }
                }
            }
        }
	}
    private Vector2 CalculateSpawnPosition(float angle, float distance)
	{
        float radianAngle = angle * Mathf.Deg2Rad;

        float x = distance * Mathf.Cos(radianAngle);
        float y = distance * Mathf.Sin(radianAngle);

        return new Vector2(x, y);
    }

    void Spawn(GameObject gobject)
	{
        int num = spawners.Count == 0 ? 1 : spawners.Count;
        float angleIncrement = 360f / num;
        for (int i = 0; i < num; i++)
		{
            float angle = Random.Range(i * angleIncrement, (i + 1) * angleIncrement);
            float distance = Random.Range(boundary.MIN_DISTANCE, boundary.MAX_DISTANCE);
            GameObject go = Instantiate(gobject, CalculateSpawnPosition(angle, distance), Quaternion.identity);
            spawners.Add(go);
            go.transform.SetParent(GameObject.Find("Spawners").transform);
            gameManager.uiHandler.uiMap.AddMapElement(go);

		}
	}

    public void AddSpawner(GameObject gobject)
	{
        Spawn(gobject);
	}
	#endregion

    public void GenerateLevel()
	{
        boundary = GameObject.FindObjectOfType<Boundary>().GetComponent<Boundary>();
        autoTile = GameObject.FindObjectOfType<AutoTile>().GetComponent<AutoTile>();

        levelSize = new Vector3Int(boundary.MAX_DISTANCE + (int)radius, boundary.MAX_DISTANCE + (int)radius, 0);
        autoTile.doSim(autoTile.numSims, levelSize);
        //StartCoroutine(SlowGenerate(Flora));
        Generate(Flora, GenerateType.Flora);
        Generate(Decals, GenerateType.Flora);
    }

	#region Unity
	// Start is called before the first frame update
	void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
        //Generate(Rocks);
        //Generate(Props);
        //Generate(Decals);
    }
	#endregion
}
