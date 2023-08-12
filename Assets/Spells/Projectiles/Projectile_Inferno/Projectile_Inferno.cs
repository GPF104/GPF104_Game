using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Inferno : MonoBehaviour
{

	#region Attributes
	Vector2 destination = new Vector2(0, 0);

    [SerializeField] float moveSpeed = 1.5f;
    [SerializeField] GameObject explosion;
    Rigidbody2D rb2d;

    AudioSource audioSource;
    [SerializeField] AudioClip shootSFX;


    #endregion

    #region Unity

    // Start is called before the first frame update
    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(shootSFX);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 destinationPoint = Vector3.Lerp(transform.position, destination, moveSpeed * Time.fixedDeltaTime);

        rb2d.MovePosition(destinationPoint);

        if (Vector3.Distance(transform.position, destination) < 0.15f)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
	#endregion
}
