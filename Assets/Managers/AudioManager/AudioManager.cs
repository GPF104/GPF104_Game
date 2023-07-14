using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] List<AudioClip> ambience = new List<AudioClip>();
    AudioSource audioSource;
    void PlayAmbience()
	{
        audioSource.clip = ambience[Random.Range(0, ambience.Count)];
        audioSource.Play();
	}
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        PlayAmbience();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
