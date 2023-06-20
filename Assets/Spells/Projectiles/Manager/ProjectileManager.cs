using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
	#region ExternalLinks 
	//	Links to external objects/classes
	ProjectileManager projectileManager;

	#endregion

	#region Attributes
	//	Variables specific to this gameobject instance

	

	#endregion

	#region Unity
	//Unity functions
	// Start is called before the first frame update
	void Start()
    {
		projectileManager = GetComponent<ProjectileManager>();
		   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		
	}

	#endregion
}
