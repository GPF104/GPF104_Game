using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject Projectile_Bullet;
    public GameObject Inferno_Bomb;
    Transform firePoint;
    public float fireForce = 20f;

    public void Fire()
    {
        GameObject bullet = Instantiate(Projectile_Bullet, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
    }



    public void Inferno()
    {
        if (GameObject.FindObjectOfType<Player>().GetComponent<Player>().scrolls > 0)
        {
            GameObject inferno = Instantiate(Inferno_Bomb, firePoint.position, firePoint.rotation);

            GameObject.FindObjectOfType<Player>().GetComponent<Player>().AddScroll(-1);
        }
        
    }

	void Start()
	{
        firePoint = GameObject.Find("FirePoint").GetComponent<Transform>();
	}
}
