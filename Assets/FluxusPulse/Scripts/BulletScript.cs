/***********************************************************************
*	Bachelor of Software Engineering
*	Media Design School
*	Auckland
*	New Zealand
*
*	(c) 2018 Media Design School
*
*	File Name	:	BulletScript.cs
*	Description	:	Projectile with linear trajectory that can deal damage to player ships.
*	Project		:	FluxusPulse
*	Team Name	:	M Breakdown Studios
***********************************************************************/
using UnityEngine;

public class BulletScript : MonoBehaviour
{
	#region Public



	// Inspector Fields

	public float speed = 1;
	public float damage = 5;

    [HideInInspector]
	public GameObject playerFired;



	#endregion Public
	#region Private



	// Unity Event Methods

	// Use this for initialization
	void Start()
	{
		Rigidbody2D rb = GetComponent<Rigidbody2D>();

		rb.velocity = transform.up * speed;

        Destroy(gameObject, 30f);
	}
	//~ fn

	void OnTriggerEnter2D(Collider2D col)
	{
        PlayerShip player = col.GetComponent<PlayerShip>();
        if (player != null)
        {
            // Damage player
            player.healthEntity.Hurt(damage);

            // Self destruct
            Destroy(gameObject);
        }
        else
        {
            EnemyScript enemy = col.GetComponent<EnemyScript>();
            if (enemy == playerFired)
            {
                // Do nothing if collision with an enemy or bullet
            }
            else
            {
                // Self destruct
                Destroy(gameObject);
            }
        }
	}
	//~ fn
    


	#endregion Private
}
//~ class
