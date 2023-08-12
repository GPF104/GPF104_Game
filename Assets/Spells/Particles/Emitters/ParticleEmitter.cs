using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmitter : MonoBehaviour
{
    ParticleSystem particles;
	private float fadeDuration = 0.25f; // Duration of the fade in seconds

    AudioSource emitter;
    [SerializeField] List<AudioClip> emitterClips = new List<AudioClip>();

    [SerializeField] bool isBubble = false;
    public bool hitWorld = true;

    private IEnumerator FadeOutAndDestroy()
    {
        float elapsedTime = 0f;
        Color originalColor = particles.main.startColor.color;
        ParticleSystem.MainModule mainModule = particles.main;

        while (elapsedTime < fadeDuration)
        {
            float normalizedTime = elapsedTime / fadeDuration;
            Color fadedColor = new Color(originalColor.r, originalColor.g, originalColor.b, 1f - normalizedTime);
            mainModule.startColor = fadedColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
    public void PlaySFX(AudioClip clip)
	{
        if (emitter != null)
		{
            emitter.PlayOneShot(clip);
        }
    }
    public void Remove()
	{
        particles.Stop();
        StartCoroutine(FadeOutAndDestroy());
    }
	// Start is called before the first frame update
	void Start()
    {
        if (this.GetComponent<AudioSource>() != null)
		{
            emitter = GetComponent<AudioSource>();
		}
        particles = this.GetComponent<ParticleSystem>();
        fadeDuration = particles.main.duration;
        if (!isBubble)
		{
            if (emitterClips.Count > 0)
			{
                PlaySFX(emitterClips[Random.Range(0, emitterClips.Count)]);
            }
            
            StartCoroutine(FadeOutAndDestroy());
        }
    }
}
