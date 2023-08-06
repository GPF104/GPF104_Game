using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LevelGenerator : MonoBehaviour
{



    #region ExternalLinks

    AutoTile autoTile;
    Boundary boundary;
    GameManager gameManager;

    #endregion

    #region Attributes

    enum GenerateType
    {
        Flora,
        Trees,
        Rocks,
        Props,
        Lighting,
        Decals,
        Obstacles,
    }
    //  Editable in Unity Editor
    [SerializeField] GameObject Spawner;
    public List<GameObject> spawners = new List<GameObject>();

    [SerializeField] float radius = 20;
    [SerializeField] int MIN_DENSITY = 100;
    [SerializeField] int MAX_DENSITY = 1000;
    [SerializeField] List<GameObject> Trees = new List<GameObject>();
    [SerializeField] List<GameObject> Flora = new List<GameObject>();
    [SerializeField] List<GameObject> Rocks = new List<GameObject>();
    [SerializeField] List<GameObject> Props = new List<GameObject>();
    [SerializeField] List<GameObject> Decals = new List<GameObject>();
    [SerializeField] List<GameObject> Shadows = new List<GameObject>();
	[SerializeField] List<GameObject> BadObstacles = new List<GameObject>();
    [SerializeField] float MIN_SCALE = 0.95f;
    [SerializeField] float MAX_SCALE = 1.5f;
    [SerializeField] float MIN_BRIGHTNESS = 0.5f;
    [SerializeField] float MAX_BRIGHTNESS = 1;
    [SerializeField] float generateTime = 0.5f;

    [SerializeField] int TrapDensity = 1500;

    //  Control the size of the generated tilemap level
    public Vector3Int levelSize = new Vector3Int(120, 120, 0);

    #endregion

    #region Generation
    //  Slower generation - Might need this for when our objects get more complex.
    IEnumerator SlowGenerate(GenerateType type)
	{
        int minimumDensity = MIN_DENSITY;
        int maximumDensity = MAX_DENSITY;

        if (type == GenerateType.Lighting)
        {
            minimumDensity += 2500;
        }

        if (type == GenerateType.Obstacles)
		{
            minimumDensity = TrapDensity;
            maximumDensity = TrapDensity + 500;
        }

        for (int i = 0; i < Random.Range(minimumDensity, maximumDensity); i++)
        {
            Vector2 pos = Random.insideUnitSphere * radius;

            if (type == GenerateType.Flora)
            {
                if (autoTile.GetTile(pos, AutoTile.TileTypes.grass))
                {
                    GameObject go = AddProp(Flora[Random.Range(0, Flora.Count)], pos, type);
                }
            }
            if (type == GenerateType.Trees)
            {
                if (autoTile.GetTile(pos, AutoTile.TileTypes.grass))
                {
                    GameObject go = AddProp(Trees[Random.Range(0, Trees.Count)], pos, type);
                }
            }
            if (type == GenerateType.Obstacles)
            {
                if (autoTile.GetTile(pos, AutoTile.TileTypes.grass))
                {
                    GameObject go = AddProp(BadObstacles[Random.Range(0, BadObstacles.Count)], pos, type);
                }
            }
            if (type == GenerateType.Lighting)
            {
                if (!autoTile.GetTile(pos, AutoTile.TileTypes.grass))
                {
                    GameObject go = AddProp(Shadows[Random.Range(0, Shadows.Count)], pos, type);
                    go.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = Random.Range(MIN_BRIGHTNESS, MAX_BRIGHTNESS);
                }
            }
        }
        yield return new WaitForSeconds(generateTime);
    }

    GameObject AddProp(GameObject prop, Vector2 pos, GenerateType type)
	{
        GameObject go = Instantiate(prop);
        float scale = Random.Range(MIN_SCALE, MAX_SCALE);
        go.transform.localScale = new Vector3(scale, scale, scale);
        go.transform.position = pos;

        if (type == GenerateType.Flora)
		{
            go.transform.SetParent(GameObject.Find("Flora").transform);
            go.transform.localScale = go.transform.localScale * 2;
        }
        if (type == GenerateType.Trees)
        {
            go.transform.SetParent(GameObject.Find("Trees").transform);
        }
        if (type == GenerateType.Lighting)
        {
            go.transform.SetParent(GameObject.Find("Lighting").transform);
        }
        if (type == GenerateType.Obstacles)
        {
            go.transform.SetParent(GameObject.Find("Obstacles").transform);
            go.transform.localScale = go.transform.localScale * 1;
        }
        if (type != GenerateType.Lighting)
		{
            SpriteRenderer sr;
            sr = go.GetComponent<SpriteRenderer>();
            sr.sortingOrder = Mathf.RoundToInt(-go.transform.position.y * 100f);
        }


        return go;
    }
    // Quick generate, does things instantly, but might need to use slowgenerate.
    void Generate(GenerateType type)
	{
        int minimumDensity = MIN_DENSITY;

        if (type == GenerateType.Lighting || type == GenerateType.Obstacles)
		{
            minimumDensity += 2500;
		}

        for (int i = 0; i < Random.Range(minimumDensity, MAX_DENSITY); i++)
		{
            Vector2 pos = Random.insideUnitSphere * radius;

            if (type == GenerateType.Flora)
			{
                if (autoTile.GetTile(pos, AutoTile.TileTypes.grass))
                {
                    GameObject go = AddProp(Flora[Random.Range(0, Flora.Count)], pos, type);
                }
            }
            if (type == GenerateType.Trees)
            {
                if (autoTile.GetTile(pos, AutoTile.TileTypes.grass))
                {
                    GameObject go = AddProp(Trees[Random.Range(0, Trees.Count)], pos, type);
                }
            }
            if (type == GenerateType.Obstacles)
            {
                if (autoTile.GetTile(pos, AutoTile.TileTypes.grass))
                {
                    GameObject go = AddProp(BadObstacles[Random.Range(0, BadObstacles.Count)], pos, type);
                }
            }
            if (type == GenerateType.Lighting)
			{
                if (!autoTile.GetTile(pos, AutoTile.TileTypes.grass))
				{
                    GameObject go = AddProp(Shadows[Random.Range(0, Shadows.Count)], pos, type);
                    go.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = Random.Range(MIN_BRIGHTNESS, MAX_BRIGHTNESS);
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

    IEnumerator SpawnSpawner(GameObject gobject)
	{

        //int num = spawners.Count == 0 ? 1 : (int)Mathf.Floor(spawners.Count / 2);
        int num = Random.Range(1, 5);
        float angleIncrement = 360f / num;
        for (int i = 0; i < num; i++)
        {
            float angle = Random.Range(i * angleIncrement, (i + 1) * angleIncrement);
            float distance = Random.Range(boundary.MIN_DISTANCE * 0.5f, boundary.MAX_DISTANCE);
            GameObject go = Instantiate(gobject, CalculateSpawnPosition(angle, distance), Quaternion.identity);
            spawners.Add(go);
            go.transform.SetParent(GameObject.Find("Spawners").transform);
            GameObject portal = gameManager.uiHandler.uiMap.AddMapElement(BlipType.portal);
            gameManager.uiHandler.uiMap.UpdateBlipPosition(portal, go.transform.position);
            yield return new WaitForSeconds(Time.deltaTime);
        }

    }
    void Spawn(GameObject gobject)
	{
        // This is causing spawners to double
        int num = spawners.Count == 0 ? 1 : (int)Mathf.Floor(spawners.Count / 2);
        float angleIncrement = 360f / num;
        for (int i = 0; i < num; i++)
		{
            float angle = Random.Range(i * angleIncrement, (i + 1) * angleIncrement);
            float distance = Random.Range(boundary.MIN_DISTANCE * 0.5f, boundary.MAX_DISTANCE);
            GameObject go = Instantiate(gobject, CalculateSpawnPosition(angle, distance), Quaternion.identity);
            spawners.Add(go);
            go.transform.SetParent(GameObject.Find("Spawners").transform);
            GameObject portal = gameManager.uiHandler.uiMap.AddMapElement(BlipType.portal);

            gameManager.uiHandler.uiMap.UpdateBlipPosition(portal, go.transform.position);
        }
	}
    public GameObject RandomSpawner()
	{
        return spawners[Random.Range(0, spawners.Count)];
	}
    public void RemoveSpawner(GameObject gobject)
	{
        spawners.Remove(gobject);
        Debug.Log("Spawner Removed " + spawners.Count);
	}
    public void AddSpawner(GameObject gobject)
	{
        //Spawn(gobject);
        StartCoroutine(SpawnSpawner(gobject));
	}
	#endregion

    public void GenerateLevel()
	{
        boundary = GameObject.FindObjectOfType<Boundary>().GetComponent<Boundary>();
        autoTile = GameObject.FindObjectOfType<AutoTile>().GetComponent<AutoTile>();

        //levelSize = new Vector3Int(boundary.MAX_DISTANCE + (int)radius, boundary.MAX_DISTANCE + (int)radius, 0);

        autoTile.doSim(autoTile.numSims, levelSize);
        //StartCoroutine(SlowGenerate(Flora));
        StartCoroutine(SlowGenerate(GenerateType.Flora));
        StartCoroutine(SlowGenerate(GenerateType.Trees));
        StartCoroutine(SlowGenerate(GenerateType.Lighting));
        StartCoroutine(SlowGenerate(GenerateType.Obstacles));
    }

	#region Unity
	// Start is called before the first frame update
	void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }
	#endregion
}
