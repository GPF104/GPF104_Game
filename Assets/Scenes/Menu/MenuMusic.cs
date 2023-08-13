using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    [SerializeField] AudioClip MusicFile;
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        //StartCoroutine(PlayMusic());
    }

}
