using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	#region ExternalLinks
    GameManager gameManager;
	#endregion
	#region Attributes

	public float moveSpeed = 10;
    Rigidbody2D rb2d;
    Weapon weapon;

    Vector2 moveDirection = new Vector2(0, 0);
    Vector2 mousePosition = new Vector2(0, 0);

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
        }

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void Move()
    {
        Vector2 aimDirection = mousePosition - rb2d.position;
        //  To-do ease in?
        rb2d.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        rb2d.rotation = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        
    }

    #endregion
    //
    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        weapon = this.gameObject.GetComponentInChildren<Weapon>();
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }
    
    void Update()
    {
        ProcessInputs();
    }

    // Framerate Independent
    void FixedUpdate() 
    {
        //Physics Calculations
        Move();
    }

    
}
