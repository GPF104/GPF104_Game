using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] GameObject BossHP;
    [SerializeField] GameObject BossObject;
    GameObject Spawner;
    GameObject Boss;

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
        Spawner = gameManager.levelGenerator.RandomSpawner();
        Boss = Instantiate(BossObject);
        Boss.transform.position = Spawner.transform.position;

	}
    public void Damaged()
	{

	}
    public void Defeated()
	{
        gameManager.uiHandler.Display(gameManager.uiHandler.bossHealthObject, false);
    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();

        
    }


}
