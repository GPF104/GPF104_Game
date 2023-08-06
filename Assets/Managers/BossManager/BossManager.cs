using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] GameObject BossHP;
    [SerializeField] GameObject BossObject;
    IEnumerator SpawnBossFX()
	{
        yield return new WaitForSeconds(1);
    }

    IEnumerator SpawnBossObjects()
    {
        yield return new WaitForSeconds(1);
    }

    IEnumerator Walk()
	{
        yield return new WaitForSeconds(1);
    }

    public void Initialize()
	{
        gameManager.uiHandler.Display(gameManager.uiHandler.bossHealthObject, true);

	}
    void Defeated()
	{

	}
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }


}
