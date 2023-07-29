using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	#region ExternalLinks
	public GameObject Projectile_Bullet;
    public GameObject Inferno_Bomb;
    public GameObject Healing_Spell;
    [SerializeField] GameObject fizzle;

	#endregion

	#region Attributes
	Transform firePoint;
    public float fireForce = 20f;

    public void Fire()
    {
        GameObject bullet = Instantiate(Projectile_Bullet, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
    }



    public void Inferno()
    {
        Instantiate(fizzle, firePoint.position, firePoint.rotation);

        if (FindObjectOfType<Player>().GetComponent<Player>().scrolls > 0)
        {
            GameObject inferno = Instantiate(Inferno_Bomb, firePoint.position, firePoint.rotation);

            FindObjectOfType<Player>().GetComponent<Player>().AddScroll(-1);
        }
        
    }

    public void Heal()
    {
        if (FindObjectOfType<Player>().GetComponent<Player>().scrolls > 0)
        {
            FindObjectOfType<Player>().GetComponent<Player>().AddScroll(-1);
        }
    }
	#endregion

	#region Unity

	void Start()
	{
        firePoint = GameObject.Find("FirePoint").GetComponent<Transform>();
	}
	#endregion
}
