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

    IEnumerator LoopMusic()
	{
        audioSource.PlayOneShot(MusicFile);
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
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(LoopMusic());
    }

    public void StopMusic()
	{
        audioSource.Stop();
	}
}
