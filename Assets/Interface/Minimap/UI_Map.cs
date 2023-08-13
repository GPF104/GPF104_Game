using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum BlipType
{
    enemy = 0,
    boss = 1,
    item = 2,
    portal = 3
}
public class UI_Map : MonoBehaviour
{

    RectTransform rectTransform;
    UIHandler uiHandler;
	List<Vector2[]> spawners = new List<Vector2[]> ();
    List<Image> spawnerIcons = new List<Image> ();
    RectTransform playerPos;
    RectTransform MapBG;
    Vector3 worldSize;
    Vector2 minimapSize;
    float xRatio;
    float yRatio;
    [SerializeField] GameObject bossTracker;
    [SerializeField] GameObject portalTracker;
    [SerializeField] GameObject enemyTracker;
    [SerializeField] GameObject itemTracker;


    List<GameObject> enemyBlipTable = new List<GameObject>();
    List<GameObject> enemyBlipList = new List<GameObject>();

    public void UpdatePositions(Vector3 position, float facing)
	{
        if (xRatio != float.NaN && yRatio != float.NaN)
		{
            playerPos.anchoredPosition = new Vector3(position.x * xRatio, position.y * yRatio, 0);
            playerPos.rotation = Quaternion.Euler(0f, 0f, facing);

        }
	}        
    public void UpdateBlipPosition(GameObject gobject, Vector3 position)
	{
        if (gobject.GetComponent<RectTransform>())
		{
            if (xRatio != float.NaN && yRatio != float.NaN)
            {
                gobject.GetComponent<RectTransform>().anchoredPosition = new Vector3(position.x * xRatio, position.y * yRatio, 0);
            }
        }
	}

    public GameObject AddMapElement(BlipType type)
	{
        Debug.Log("Add map element: " + type);
        GameObject blip = null;

        if (type == BlipType.enemy)
		{
            blip = Instantiate(enemyTracker);
        }
        if (type == BlipType.portal)
		{
            blip = Instantiate(portalTracker);
		}
        if (type == BlipType.item)
		{
            blip = Instantiate(itemTracker);
		}
        if (type == BlipType.boss)
		{
            blip = Instantiate(bossTracker);
		}
        //go.transform.SetParent(GameObject.Find("Spawners").transform);
        blip.transform.SetParent(MapBG.transform);
        return blip;
	}

    IEnumerator LoadMap()
	{
        yield return new WaitForSeconds(0.25f);

        uiHandler = GameObject.FindObjectOfType<UIHandler>().GetComponent<UIHandler>();
        worldSize = uiHandler.gameManager.levelGenerator.levelSize;
        minimapSize = MapBG.rect.size;
        xRatio = minimapSize.x / worldSize.x;
        yRatio = minimapSize.y / worldSize.y;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadMap());
        

        MapBG = GameObject.Find("MapBG").GetComponent<RectTransform>();
        rectTransform = GameObject.Find("MapBG").GetComponent<RectTransform>();
        playerPos = GameObject.Find("PlayerPt").GetComponent<RectTransform>();
    }


}
