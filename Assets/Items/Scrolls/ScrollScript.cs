using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour
{
    public int value = 1;

    GameObject blip;
    AudioSource audioSource;
    [SerializeField] AudioClip spawnSFX;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (spawnSFX != null)
        {
            audioSource.PlayOneShot(spawnSFX);
        }
        if (blip == null)
        {
            blip = GameObject.FindObjectOfType<GameManager>().uiHandler.uiMap.AddMapElement(BlipType.item);
            GameObject.FindObjectOfType<GameManager>().uiHandler.uiMap.UpdateBlipPosition(blip, this.transform.position);
        }
    }
    void OnDestroy()
    {
        Destroy(blip);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().AddScroll(value);
            Destroy(gameObject);
        }
    }
}
