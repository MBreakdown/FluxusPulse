﻿/***********************************************************************
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
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyScript : MonoBehaviour
{
    #region Public



    // Properties

    public HealthEntity healthEntity { get; private set; }
    public Rigidbody2D rb { get; private set; }



    // Inspector Fields

    // Reference variable, assignable in the inspector.
    [Range(1, 2)]
    public int playerIndexToFollow = 1;
	public BulletScript bulletPrefab;
    public GameObject minePrefab;
	public float speed;
	public float maxSpeed;
	public float damage;
	public float reward;
	public bool ranged;
    public bool selfDestruct;
    public bool bomb;
    private float bulletTime = 0;
    public float maxBulletTime = 0.75f;
    private float mineTime = 0;
    private float maxMineTime = 6f;
    public AudioSource explosion;
    public AudioSource hurt;
    public AudioSource shoot;

	[Header("Effects")]
	public GameObject pinkPart;
	public GameObject redPart;
	public GameObject greenPart;

    #endregion Public
    #region Private



    // Unity Event Methods

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        healthEntity = GetComponent<HealthEntity>();
        
        maxMineTime = maxMineTime + Random.Range(0f, 6f);
    }
    //~ fn

    void Start()
    {
        EnemyManager.Instance.OnEnemySpanwed(this);
    }
    //~ fn


	// Runs once every physics frame.
	// Use Time.fixedDeltaTime here
	// Use Rigidbody stuff here.
	void FixedUpdate()
    {
        bulletTime = Mathf.Clamp(bulletTime + Time.fixedDeltaTime, 0, maxBulletTime);
        mineTime = Mathf.Clamp(mineTime + Time.fixedDeltaTime, 0, maxMineTime);

        if (GameController.IsGameInProgress == false)
            return;

		// Fly towards the player

        PlayerShip player = PlayerShip.GetPlayer(playerIndexToFollow);

        // Set the vector to the player that they're attacking
        Vector2 vectorToPlayer = player.transform.position - this.transform.position;

        // Figure out if the enemy needs to stay away from the player
        if (Vector2.Distance(this.transform.position, player.transform.position) < 15 && ranged == true)
		{
			// Stop movement
			rb.velocity = Vector2.zero;

            if (bulletTime >= maxBulletTime)
            {
                bulletTime = 0;

                // Fire bullet
                BulletScript bullet = Instantiate(
                    bulletPrefab,
                    new Vector2(transform.position.x, transform.position.y),
                    Quaternion.LookRotation(Vector3.forward, vectorToPlayer)
                );
                bullet.name = bulletPrefab.name;
				bullet.originEnemy = this;

                // Play shoot sound
                FindObjectOfType<GameController>().shoot.Play();
			}
		}
		else
		{
			// Fly towards the player
			rb.AddForce(vectorToPlayer.normalized * speed);

            // Check to spawn bombs
            if (bomb)
            {
                if (mineTime >= maxMineTime)
                {
                    mineTime = 0;

                    // Spawn mine
                    GameObject mine = Instantiate(
                        minePrefab,
                        new Vector3(transform.position.x, transform.position.y, 10),
                        Quaternion.LookRotation(Vector3.forward, vectorToPlayer)
                    );
                    mine.gameObject.GetComponent<MineScript>().playerToHurt = PlayerShip.GetPlayer(playerIndexToFollow).gameObject;
                    mine.name = minePrefab.name;

                    // Reset random
                    maxMineTime = maxMineTime + Random.Range(0f, 6f);

                    // Play deploy sound
                    FindObjectOfType<GameController>().deploy.Play();
                }
            }
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
        // Don't let ranged enemies deal melee damage
        if (ranged)
            return;

        // Run collision with player to follow
        PlayerShip player = col.gameObject.GetComponent<PlayerShip>();
        if (player && player.PlayerIndex == playerIndexToFollow)
		{
            // Play hurt sound, unless invincible
            if (!player.healthEntity.Invincible)
                FindObjectOfType<GameController>().hurt.Play();
			Instantiate (redPart, this.transform.position, this.transform.rotation);

            // Damage player
            player.healthEntity.Hurt(damage);

            if (selfDestruct == true && !player.healthEntity.Invincible)
            {
                // Check if the enemy is about to die
                if (this.healthEntity.Health == 1)
                {
                    // Play explosion sound
                    FindObjectOfType<GameController>().explosion.Play();
                }

                // Damage self
                this.healthEntity.Hurt(1);
            }
		}
	}
	//~ fn

	// Detect an enemy death
	void OnDestroy()
	{
        if (!GameController.IsGameInProgress)
            return;

        // Decrease the number of enemies
        EnemyManager.Instance.OnEnemyDestroyed(this);
	}
	//~ fn



	#endregion Private
}
//~ class
