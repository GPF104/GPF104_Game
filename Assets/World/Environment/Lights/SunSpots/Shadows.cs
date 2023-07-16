using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadows : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0.25f;
    IEnumerator Move(float moveSpeed)
    {
        yield return new WaitForSeconds(0.05f);
        float randomOffset = Random.Range(0.5f, 1.2f);

        Vector3 originalPosition = transform.position; // Store the original position

        float elapsedTime = 0f;
        float swayDuration = 2f; // Adjust the duration of the sway here

        Vector3 initialPosition = transform.position; // Store the initial position
        Vector3 targetPosition = originalPosition; // Set the target position to the original position

        while (elapsedTime < swayDuration)
        {
            float swayAmount = Mathf.Sin(elapsedTime * Mathf.PI * 2 / swayDuration) * randomOffset;
            transform.position = initialPosition + Vector3.right * swayAmount; // Adjust the direction of the sway here

            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }

        // Smoothly transition back to the original position
        float transitionDuration = 0.5f; // Adjust the duration of the transition here
        elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);

            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }

        transform.position = targetPosition; // Ensure the final position is set correctly

        StartCoroutine(Move(moveSpeed));
    }




    IEnumerator Wait()
	{
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(Move(moveSpeed));
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait());

    }
}
