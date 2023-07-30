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
        Fade("in");
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

    IEnumerator FadeOut()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float vol = Mathf.Lerp(audioSource.volume, 0f, timer / fadeDuration);
            Debug.Log(vol);
            audioSource.volume = vol;
            yield return null;
        }
        audioSource.Stop();
    }

    IEnumerator FadeIn()
    {
        audioSource.PlayOneShot(audioSource.clip);
        audioSource.volume = 0f;
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(audioSource.volume, startingVolume, timer / fadeDuration);
            yield return null;
        }
    }
    public void SetClip(AudioClip clip)
	{
        StopAllCoroutines();
        audioSource.clip = clip;
        audioSource.Play();
	}
    public void Fade(string input)
    {
        if (input == "out")
        {
            StartCoroutine(FadeOut());
        }
        else if (input == "in")
        {
            StartCoroutine(FadeIn());
        }
    }
    
    public void StopMusic()
	{
        audioSource.Stop();
	}
}
