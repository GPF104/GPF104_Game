using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameManager gameManager;

    IEnumerator UpdatePosition()
	{
        yield return new WaitForSeconds(0.5f);
        gameManager.uiHandler.uiMap.UpdatePositions(this.transform.position);
        StartCoroutine(UpdatePosition());
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
        StartCoroutine(UpdatePosition());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //if (other.gameObject.CompareTag("EnemyBullet")) ;
    }
}
