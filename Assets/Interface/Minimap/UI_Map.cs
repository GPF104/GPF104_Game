using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void UpdatePositions(Vector3 position)
	{
        if (xRatio != float.NaN && yRatio != float.NaN)
		{
            Debug.Log(xRatio + " " + yRatio);
            playerPos.anchoredPosition = new Vector3(position.x * xRatio, position.y * yRatio, 0);
        }
	}        

    public void AddMapElement(GameObject gobject)
	{

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
