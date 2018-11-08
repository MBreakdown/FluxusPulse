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

[RequireComponent(typeof(HealthEntity))]
public class EnemyScript : MonoBehaviour
{
    #region Public



    // Properties

    public HealthEntity healthEntity { get; private set; }



    // Inspector Fields

    // Reference variable, assignable in the inspector.
    [Range(1, 2)]
    public int playerIndexToFollow = 1;
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

    void Awake()
    {
        healthEntity = GetComponent<HealthEntity>();
    }
    //~ fn


	// Runs once every physics frame.
	// Use Time.fixedDeltaTime here
	// Use Rigidbody stuff here.
	void FixedUpdate()
	{
		// Fly towards the player
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		if (!rb)
			Debug.LogError("Rigidbody missing!", this);

		// Set the vector to the player that they're attacking
		Vector2 vectorToPlayer = PlayerShip.GetPlayer(playerIndexToFollow).transform.position - this.transform.position;

		// Figure out if the enemy needs to stay away from the player
		if (Vector2.Distance(this.transform.position, PlayerShip.GetPlayer(playerIndexToFollow).transform.position) < 15 && ranged == true)
		{
			// Stop movement
			rb.velocity = Vector2.zero;

			time += Time.fixedDeltaTime;
			if (time >= 0.75)
			{
				time = 0;
				
				// Fire bullet
				GameObject bullet = Instantiate(referenceBullet, new Vector2(transform.position.x, transform.position.y), Quaternion.LookRotation(Vector3.forward, vectorToPlayer));
                bullet.name = referenceBullet.name;
				bullet.GetComponent<BulletScript>().playerFired = gameObject;
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

	// Run collisions
	void OnCollisionEnter2D(Collision2D col)
	{
        // Run collision with player to follow
        PlayerShip player = col.gameObject.GetComponent<PlayerShip>();
        if (player == PlayerShip.GetPlayer(playerIndexToFollow))
		{
            // Damage player
            player.healthEntity.Hurt(damage);

            if (selfDestruct == true)
            {
                // Damage self
                this.healthEntity.Kill();
            }
		}
	}
	//~ fn

	// Detect an enemy death
	void OnDestroy()
	{
        // Decrease the number of enemies
        EnemyManager.Instance.enemyCount -= 1;
	}
	//~ fn



	#endregion Private
}
//~ class
