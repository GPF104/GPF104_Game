using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	#region ExternalLinks

	GameManager gameManager;
	TimeManager timeManager;
	#endregion

	#region Attributes

	#endregion

	#region Unity
	// Start is called before the first frame update
	void Start()
    {
        gameManager = this.GetComponentInParent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	#endregion
}
