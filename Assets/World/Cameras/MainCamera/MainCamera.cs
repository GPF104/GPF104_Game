using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    Camera cam;

	[SerializeField] GameObject target;

    [SerializeField] float followSpeed = 2;
    Vector3 newPosition = new Vector3(0, 0, -10);
    // Start is called before the first frame update
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
