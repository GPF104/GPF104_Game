using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] List<AudioClip> ambience = new List<AudioClip>();
	[SerializeField] List<AudioClip> music = new List<AudioClip> ();
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
        //GameObject.FindGameObjectWithTag("Music").GetComponent<MusicPlayer>().SetClip(music[0]);
        //GameObject.FindGameObjectWithTag("Music").GetComponent<MusicPlayer>().Fade("in");
    }

    public AudioClip RandomMusic()
	{
        return music[Random.Range(0, music.Count)];
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
