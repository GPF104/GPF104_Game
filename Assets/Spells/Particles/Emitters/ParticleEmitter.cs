using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmitter : MonoBehaviour
{
    private ParticleSystem particleSystem;
	private float fadeDuration = 0.25f; // Duration of the fade in seconds

    private IEnumerator FadeOutAndDestroy()
    {
        Debug.Log("Destroy");
        float elapsedTime = 0f;
        Color originalColor = particleSystem.main.startColor.color;
        ParticleSystem.MainModule mainModule = particleSystem.main;

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
        particleSystem.Stop();
        StartCoroutine(FadeOutAndDestroy());
    }
	// Start is called before the first frame update
	void Start()
    {
        particleSystem = this.GetComponent<ParticleSystem>();
        fadeDuration = particleSystem.main.duration;
    }
}
