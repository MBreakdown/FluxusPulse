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

	public float speed;
	public float damage;
	public GameObject playerFired;

	#endregion Public
	#region Private

	// Unity Event Methods

	// Use this for initialization
	void Start()
	{
		Rigidbody2D rb = GetComponent<Rigidbody2D>();

		rb.velocity = transform.up * speed;
	}
	//~ fn
	
	// Fixed update is called once every physics frame
	void FixedUpdate()
	{

	}
	//~ fn

	// Update is called once per frame
	void Update()
	{

	}
	//~ fn

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.GetComponent<PlayerShip>() != null)
		{
			// Damage player
			col.gameObject.GetComponent<HealthEntity>().Hurt(damage);

			// Self destruct
			Destroy(this.gameObject);
		}
		else if (col.gameObject.GetComponent<EnemyScript>().gameObject == playerFired)
		{
			// Do nothing if collision with an enemy or bullet
		}
		else
		{
			// Self destruct
			Destroy(this.gameObject);
		}
	}
	//~ fn
    
	#endregion Private
}
//~ class
