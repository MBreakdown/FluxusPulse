/***********************************************************************
*	Bachelor of Software Engineering
*	Media Design School
*	Auckland
*	New Zealand
*
*	(c) 2018 Media Design School
*
*	File Name	:	EnemyScript.cs
*	Description	:	An enemy that follows and damages the target player.
*	Project		:	FluxusPulse
*	Team Name	:	M Breakdown Studios
***********************************************************************/
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
	#region Public



	// Inspector Fields

	// Reference variable, assignable in the inspector.
	public Transform playerToFollow;
	public Transform playerToAvoid;
	public GameObject referenceBullet;
	public float speed;
	public float maxSpeed;
	public float damage;
	public float reward;
	public bool ranged;
	public float time = 0;
    public bool selfDestruct;



	#endregion Public
	#region Private



	// Unity Event Methods

	// Runs once every physics frame.
	// Use Time.fixedDeltaTime here
	// Use Rigidbody stuff here.
	void FixedUpdate()
	{
		// Check if the player exists
		if (!playerToFollow)
		{
			return;
		}
		// Fly towards the player
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		if (!rb)
			Debug.LogError("Rigidbody missing!", this);

		// Set the vector to the player that they're attacking
		Vector2 vectorToPlayer = playerToFollow.transform.position - this.transform.position;

		// Figure out if the enemy needs to stay away from the player
		if (Vector2.Distance(this.transform.position, playerToFollow.transform.position) < 15 && ranged == true)
		{
			// Stop movement
			rb.velocity = Vector2.zero;

			time += Time.fixedDeltaTime;
			if (time >= 0.75)
			{
				time = 0;
				
				// Fire bullet
				GameObject bullet = Instantiate(referenceBullet, new Vector2(transform.position.x, transform.position.y), Quaternion.LookRotation(Vector3.forward, vectorToPlayer));
				bullet.gameObject.GetComponent<BulletScript>().playerFired = gameObject;
			}
		}
		else
		{
			// Fly towards the player
			rb.AddForce(vectorToPlayer.normalized * speed);
		}
		
		// Ensure max speed
		if (rb.velocity.sqrMagnitude > (maxSpeed * maxSpeed))
		{
			rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
		}

		// Rotate towards the correct direction
		rb.rotation = Mathf.Rad2Deg * Mathf.Atan2(vectorToPlayer.y, vectorToPlayer.x) - 90;
	}

	// Update is called once per frame.
	// Use Time.deltaTime here.
	// Use game input here.
	void Update()
	{

	}
	//~ fn

	// Run collisions
	void OnCollisionEnter2D(Collision2D col)
	{
        // Check player's existance to avoid errors
		if (!playerToFollow)
		{
			return;
		}
		// Run collision with player to follow
		if (col.gameObject.name == playerToFollow.name)
		{
			// Damage player
			col.gameObject.GetComponent<HealthEntity>().Hurt(damage);

            if (selfDestruct == true)
            {
                // Damage self
                this.gameObject.GetComponent<HealthEntity>().Hurt(1);
            }
		}
	}
	//~ fn

	// Detect an enemy death
	void OnDestroy()
	{
		// Decrease the number of enemies
		FindObjectOfType<EnemyManager>().enemyCount -= 1;
	}
	//~ fn



	#endregion Private
}
//~ class