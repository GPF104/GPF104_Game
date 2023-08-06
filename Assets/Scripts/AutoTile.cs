using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class AutoTile : MonoBehaviour
{
	#region Attributes
	public enum TileTypes
	{
        grass,
        dirt,
        rock,
        path
	}

    [Range(1, 10)]
    public int numSims;
    //  Editor Variables
    [Range(0, 100)]
    public int iniChance;
    [Range(1, 8)]
    public int birthLimit;
    [Range(1, 8)]
    public int deathLimit;

    [Range(1, 10)]
    public int numR;

    public int[,] terrainMap;
    
    public Tilemap topMap;
    public Tilemap botMap;
    public RuleTile topTile;
    public RuleTile botTile;

    int width;
    int height;

    public void RemoveTile(Vector3Int tilePos)
	{
        Debug.Log("Remove Tile " + tilePos);
	}
    public void doSim(int nu, Vector3Int tmpSize)
    {
        clearMap(false);
        width = tmpSize.x;
        height = tmpSize.y;

        if (terrainMap == null)
        {
            terrainMap = new int[width, height];
            initPos();
        }

        for (int i = 0; i < nu; i++)
        {
            terrainMap = genTilePos(terrainMap);
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int tilePosition = new Vector3Int(-x + width / 2, -y + height / 2, 0);
                if (terrainMap[x, y] == 1)
				{
                    Vector3 worldPosition = topMap.CellToWorld(tilePosition);
                    Collider2D[] overlapColliders = Physics2D.OverlapPointAll(worldPosition);
                    bool hasSpawn = false;
                    foreach (Collider2D collider in overlapColliders)
                    {
                        if (collider.gameObject.CompareTag("NoSpawn"))
                        {
                            hasSpawn = true;
                            break;
                        }
                    }

                    if (hasSpawn)
					{
                        botMap.SetTile(tilePosition, botTile);
                        continue;
                    }
                    else
					{
                        topMap.SetTile(tilePosition, topTile);
                    }
                    
                }
                    
                botMap.SetTile(tilePosition, botTile);
            }
        }
    }

    public void initPos()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                terrainMap[x, y] = Random.Range(1, 101) < iniChance ? 1 : 0;
            }
        }
    }

    //  int vector
    public int[,] genTilePos(int[,] oldMap)
    {
        int[,] newMap = new int[width, height];
        int neighb;
        BoundsInt myB = new BoundsInt(-1, -1, 0, 3, 3, 1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                neighb = 0;
                foreach (var b in myB.allPositionsWithin)
                {
                    if (b.x == 0 && b.y == 0) continue;
                    if (x + b.x >= 0 && x + b.x < width && y + b.y >= 0 && y + b.y < height)
                    {
                        neighb += oldMap[x + b.x, y + b.y];
                    }
                    else
                    {
                        neighb++;
                    }
                }
                if (oldMap[x, y] == 1)
                {
                    if (neighb < deathLimit) newMap[x, y] = 0;

                    else
                    {
                        newMap[x, y] = 1;

                    }
                }
                if (oldMap[x, y] == 0)
                {
                    if (neighb > birthLimit) newMap[x, y] = 1;

                    else
                    {
                        newMap[x, y] = 0;
                    }
                }

            }

        }
        return newMap;
    }

    public bool GetTile(Vector3 position, TileTypes tileType)
	{
        if (tileType == TileTypes.grass)
		{
            return topMap.GetTile(Vector3Int.FloorToInt(position));
		}

        return topMap.GetTile(Vector3Int.FloorToInt(position));
	}
    
    public IEnumerator UnsetTile(Vector2 position, float radius)
    {
        LayerMask grassLayerMask = LayerMask.GetMask("grassLayer");

        // Get the bounds of the circle area in world space
        Bounds circleBounds = new Bounds(position, new Vector3(radius * 2, radius * 2, 0));

        // Get the tile positions within the circle bounds
        Vector3Int minCell = topMap.WorldToCell(circleBounds.min);
        Vector3Int maxCell = topMap.WorldToCell(circleBounds.max);

        for (int x = minCell.x; x <= maxCell.x; x++)
        {
            for (int y = minCell.y; y <= maxCell.y; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);

                if (circleBounds.Contains(topMap.GetCellCenterWorld(tilePosition)))
                {
                    topMap.SetTile(tilePosition, null);
                }
                yield return new WaitForSeconds(Time.deltaTime);
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }

        // Remove props

        LayerMask objectLayerMask = LayerMask.GetMask("World", "Traps");

        RaycastHit2D[] objectHits = Physics2D.CircleCastAll(position, radius, Vector2.zero, Mathf.Infinity, objectLayerMask);

        foreach (RaycastHit2D objectHit in objectHits)
        {
            // Remove objects with specific tags
            if (objectHit.collider != null)
			{
                GameObject objectToRemove = objectHit.collider.gameObject;
                Destroy(objectToRemove);
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    
    public void clearMap(bool complete)
    {

        topMap.ClearAllTiles();
        botMap.ClearAllTiles();
        if (complete)
        {
            terrainMap = null;
        }
    }
	#endregion
}
