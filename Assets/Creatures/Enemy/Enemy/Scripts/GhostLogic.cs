using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostLogic : MonoBehaviour
{
    BoxCollider2D boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

	private void OnTriggerStay2D(Collider2D collision)
	{
		
	}
}
