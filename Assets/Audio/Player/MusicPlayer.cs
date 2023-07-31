using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    AudioSource audioSource;
    [SerializeField] AudioClip MusicFile;
    [SerializeField] public bool isArena = false;
    private static MusicPlayer instance = null;
    public static MusicPlayer Instance
    {
        get { return instance; }
    }

    [SerializeField] float startingVolume;
    IEnumerator LoopMusic()
	{
        audioSource.clip = MusicFile;
        audioSource.Play();
        yield return new WaitForSeconds(MusicFile.length);
        StartCoroutine(LoopMusic());
    }

    void Awake()
    {
        if (!isArena)
		{
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                instance = this;
            }
            DontDestroyOnLoad(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        startingVolume = audioSource.volume;
        if (!isArena)
		{
            StartCoroutine(LoopMusic());
        }
        else
		{
            this.GetComponent<AudioListener>().enabled = false;
        }
    }

	public void SetEnabled(bool input)
	{
        this.GetComponent<AudioListener>().enabled = input;
    }
	public void SetClip(AudioClip input)
	{
        StopCoroutine(LoopMusic());
        MusicFile = input;
        StartCoroutine(LoopMusic());
	}

    public void StopMusic()
	{
        audioSource.Stop();
	}
}
