using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] List<AudioClip> ambience = new List<AudioClip>();
    AudioSource audioSource;

    IEnumerator PlayAmbience()
	{
        audioSource.clip = ambience[Random.Range(0, ambience.Count)];
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        StartCoroutine(PlayAmbience());
    }
    public void Initialize()
	{
        StartCoroutine(PlayAmbience());
    }

    public void PlayAudio(AudioClip clip)
	{
        audioSource.PlayOneShot(clip);
	}
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }
}
