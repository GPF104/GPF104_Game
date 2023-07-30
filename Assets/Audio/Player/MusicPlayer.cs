using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    AudioSource audioSource;
    [SerializeField] AudioClip MusicFile;

    private static MusicPlayer instance = null;
    public static MusicPlayer Instance
    {
        get { return instance; }
    }

    [SerializeField] float fadeDuration = 2f;
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
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Music").Length > 0)
		{
            Destroy(this);
		}
        audioSource = GetComponent<AudioSource>();
        startingVolume = audioSource.volume;
        StartCoroutine(LoopMusic());

    }


    public void SetClip(AudioClip clip)
	{
        StopAllCoroutines();
        audioSource.clip = clip;
        audioSource.Play();
	}

    public void StopMusic()
	{
        audioSource.Stop();
	}
}
