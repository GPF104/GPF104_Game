using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InfernoBullet : MonoBehaviour
{
    public float speed = 5f;

    private Vector3 target;
    private Vector3 position1;
    private Vector3 position2;
    
    void Start()
    {
        target = transform.position;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

    }

}
