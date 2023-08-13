using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	#region ExternalLinks

    GameManager gameManager;
    Player player;
    #endregion
    #region Attributes

    [SerializeField] private Transform playerTransform;
    [SerializeField] private LayerMask objectLayerMask;

    Animator animator;
	public float moveSpeed = 10;
    public float cooldown = 0.1f;
    public float fireRate = 0.25f;
    Rigidbody2D rb2d;
    Weapon weapon;
    private SpriteRenderer tempRend;

    Vector2 moveDirection = new Vector2(0, 0);
    Vector2 mousePosition = new Vector2(0, 0);
    IEnumerator FireAnim(float cooldown)
	{
        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(cooldown);
        animator.SetBool("isAttacking", false);
        if (Input.GetMouseButton(0)) 
        {
            StartCoroutine(FireAnim(cooldown));
        }
    }
    
    void ProcessInputs()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
		{
            if (gameManager.GamePaused)
			{
                gameManager.Unpause();
			}
            else
			{
                gameManager.Pause();
			}
		}

        if (gameManager.GamePaused == false && gameManager.GameFinished == false)
		{
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            
            if (Input.GetMouseButtonDown(0))
            {
                weapon.Fire();
                StartCoroutine(weapon.KeepFiring(fireRate));
                StartCoroutine(FireAnim(cooldown));
            }
            
            //Inferno Setup
            if (Input.GetMouseButtonDown(1)) //need to create spin wheel
            {
                StartCoroutine(FireAnim(cooldown));
                weapon.Inferno();
            }

            // Push Button Heal Setup

            if (Input.GetKeyDown(KeyCode.Q))
			{
                player.Heal();
			}
            /*heal setup
            if (Input.GetMouseButtonDown(1))
            {
                weapon.Heal();
            }*/

            moveDirection = new Vector2(moveX, moveY).normalized;
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }    

    void Move()
    {
        if (this.GetComponent<Player>().isPushed == false)
		{
            Vector2 aimDirection = mousePosition - rb2d.position;
            //  To-do ease in?
            rb2d.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
            rb2d.rotation = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        }
    }

    private void UpdateSortingOrder()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Get the player's y-position and the tree object's y-position
        float playerY = playerTransform.position.y;
        float treeY = transform.position.y;

        // Set the initial sorting order based on the y-position and player position relative to the tree object
        int sortingOrder = Mathf.RoundToInt(-treeY * 100f);
        if (playerY > treeY)
        {
            sortingOrder -= 1;
        }
        spriteRenderer.sortingOrder = sortingOrder;
    }

    #endregion
    //
    #region Unity
    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        weapon = this.gameObject.GetComponentInChildren<Weapon>();
        animator = GetComponent<Animator>();
        tempRend = this.GetComponent<SpriteRenderer>();
        player = this.gameObject.GetComponent<Player>();

        if (this.gameObject.GetComponent<Player>().isDev == false)
		{
            gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
        }
    }

    

    void Update()
    {
        ProcessInputs();
        UpdateSortingOrder();
    }


    // Framerate Independent
    void FixedUpdate() 
    {
        //Physics Calculations
        Move();
    }
	#endregion
}
