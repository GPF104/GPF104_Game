using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProp : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        int sortingOrder = Mathf.RoundToInt(-this.transform.position.y * 100f);

        // Apply the sorting order to the player sprite
        spriteRenderer.sortingOrder = sortingOrder;
        GameObject.Find("TowerTop").GetComponent<SpriteRenderer>().sortingOrder = spriteRenderer.sortingOrder - 1;
    }
}
