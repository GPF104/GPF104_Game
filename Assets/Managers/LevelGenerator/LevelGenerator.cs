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

	#endregion

	#region Attributes

	//  Edit in Unity Editor
	[SerializeField] float radius = 20;
    [SerializeField] int MIN_DENSITY = 100;
    [SerializeField] int MAX_DENSITY = 1000;
	[SerializeField] List<GameObject> Flora = new List<GameObject>();
    [SerializeField] List<GameObject> Rocks = new List<GameObject>();
    [SerializeField] List<GameObject> Props = new List<GameObject>();
    [SerializeField] List<GameObject> Decals = new List<GameObject>();

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
                        go.transform.position = pos;
                    }
                }
            }
        }
	}
	#endregion

	#region Unity
	// Start is called before the first frame update
	void Start()
    {
        autoTile = GameObject.FindObjectOfType<AutoTile>().GetComponent<AutoTile>();
        autoTile.doSim(autoTile.numSims);
        //StartCoroutine(SlowGenerate(Flora));
        Generate(Flora, GenerateType.Flora);
        //Generate(Rocks);
        //Generate(Props);
        //Generate(Decals);
    }
	#endregion
}
