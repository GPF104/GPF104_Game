using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    Camera cam;

    [SerializeField] public bool isArena = false;

    [SerializeField] GameObject target;

    [SerializeField] float followSpeed = 2;
    Vector3 newPosition = new Vector3(0, 0, -10);
    [SerializeField] AnimationCurve curve;

    IEnumerator Shaking(float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = newPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        this.transform.position = newPosition;
    }
    public void DoShake(float duration)
	{
        StartCoroutine(Shaking(duration));
	}

    public void StopShake()
	{
        StopAllCoroutines();
	}
    // Start is called before the first frame update

    public void StartListening()
	{
        this.GetComponent<AudioListener>().enabled = true;
	}
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        newPosition = new Vector3(Mathf.Lerp(this.transform.position.x, target.transform.position.x, Time.deltaTime * followSpeed), Mathf.Lerp(this.transform.position.y, target.transform.position.y, Time.deltaTime * followSpeed), -10);
        this.transform.position = newPosition;
    }
}
