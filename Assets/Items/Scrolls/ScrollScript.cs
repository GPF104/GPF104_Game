using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour
{
    public int value = 1;

    GameObject blip;
    AudioSource audioSource;
    [SerializeField] AudioClip spawnSFX;

    public float bobSpeed = 1.0f;  // Speed of bobbing motion
    public float bobHeight = 0.2f; // Height of bobbing motion

    private Vector3 originalPosition;

    IEnumerator Bob()
    {
        float elapsedTime = 0.0f;

        while (true)
        {
            float newY = originalPosition.y + Mathf.Sin(elapsedTime * bobSpeed) * bobHeight;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
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
        originalPosition = transform.position;
        StartCoroutine(Bob());
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
