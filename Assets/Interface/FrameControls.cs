using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Fade
{
    In,
    Out
}
public class FrameControls : MonoBehaviour
{
    public void FrameFade(GameObject gobject, Fade fade, float speed)
    {
        StartCoroutine(FadeFrame(gobject, fade, speed));
    }

    public void FrameOpacity(GameObject gobject, float opacity)
	{
        SpriteRenderer spriteRenderer = gobject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, opacity);
    }

    IEnumerator FadeFrame(GameObject gobject, Fade fade, float speed)
    {
        SpriteRenderer spriteRenderer = gobject.GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        Color targetColor;

        float elapsedTime = 0f;

        if (fade == Fade.In)
            targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
        else
            targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        while (elapsedTime < speed)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(elapsedTime / speed);

            spriteRenderer.color = Color.Lerp(originalColor, targetColor, normalizedTime);

            yield return null;
        }

        // Ensure that the final color is set correctly
        spriteRenderer.color = targetColor;
    }
}
