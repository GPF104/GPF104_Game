using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmitter : MonoBehaviour
{
    ParticleSystem particles;
	private float fadeDuration = 0.25f; // Duration of the fade in seconds

    AudioSource emitter;
    [SerializeField] AudioClip emitterClip;
    [SerializeField] bool isBubble = false;

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
            emitter.PlayOneShot(emitterClip);
		}
        particles = this.GetComponent<ParticleSystem>();
        fadeDuration = particles.main.duration;
        if (!isBubble)
		{
            StartCoroutine(FadeOutAndDestroy());
        }
    }
}
