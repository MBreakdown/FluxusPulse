/***********************************************************************
*	Bachelor of Software Engineering
*	Media Design School
*	Auckland
*	New Zealand
*
*	(c) 2018 Media Design School
*
*	File Name	:	PlayerShip.cs
*	Description	:	The object that a player controls.
*	Project		:	FluxusPulse
*	Team Name	:	M Breakdown Studios
*	Author		:	Elijah Shadbolt
*	Mail		:	elijah.sha7979@mediadesign.school.nz
***********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Types

[Serializable]
public class SpeedBlock
{
    public float moveSpeed = 10;
    public float rotateSpeed = 200;
}
//~ class

public enum BoostState
{
	None,

	// Increased speed for a short time.
	Boosting,

	// Waiting until it can be used again.
	Cooldown,
}
//~ enum

public enum FlingState
{
	None,

	// Rotating around a pivot point with increased speed.
	Flinging,

	// Flying forwards with high speed.
	Flung,

	// Cooldown after flinging.
	Cooldown,
}
//~ enum

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(HealthEntity))]
public class PlayerShip : MonoBehaviour
{
    #region Public



    // Public Static Methods
    
    public static PlayerShip GetPlayer(int playerIndex)
    {
        if (playerIndex < 1 || playerIndex > s_Players.Length)
            throw new ArgumentOutOfRangeException(nameof(playerIndex), playerIndex, "Player Index out of range.");

        int i = playerIndex - 1;
        if (s_Players[i] == null)
        {
            s_Players[i] = FindObjectsOfType<PlayerShip>().FirstOrDefault(x => x.PlayerIndex == playerIndex);
            if (!s_Players[i] && GameController.IsGameInProgress)
            {
                Debug.LogError("There is not a Player " + playerIndex + " in the scene.");
            }
        }
        return s_Players[i];
    }
    //~ fn


    
    // Public Static Properties

    public static PlayerShip GetPlayer1 { get { return GetPlayer(1); } }
    public static PlayerShip GetPlayer2 { get { return GetPlayer(2); } }



    // Public Properties

    public string InputNameSuffix => PlayerIndex.ToString();

	public float GetVerticalAxis { get { return Input.GetAxis("Vertical" + InputNameSuffix); } }
	public float GetHorizontalAxis { get { return Input.GetAxis("Horizontal" + InputNameSuffix); } }
	public bool GetBoostButton { get { return Input.GetButton("Fire1" + InputNameSuffix); } }
	public bool GetTetherButton { get { return Input.GetButton("Fire2" + InputNameSuffix); } }
	public bool GetBombButton { get { return Input.GetButton("Fire3" + InputNameSuffix); } }


	public bool IsBoosting { get { return BoostState == BoostState.Boosting; } }

	public bool IsFlinging { get { return FlingState == FlingState.Flinging; } }
	public bool IsFlung { get { return FlingState == FlingState.Flung; } }
    public bool IsFlingingOrFlung { get { return IsFlinging || IsFlung; } }



    // Public Auto Properties

    public Rigidbody2D rb { get; private set; }
    public HealthEntity healthEntity { get; private set; }

    public BoostState BoostState { get; private set; } = BoostState.None;

	public FlingState FlingState { get; private set; } = FlingState.None;
	public Transform FlingPivot { get; private set; } = null;
	public bool IsFlingDirectionClockwise { get; private set; } = false;



	// Public Constants

	public const string FlingPivotTag = "FlingPivot";



	// Inspector Fields

	[Range(1, 2)]
	public int PlayerIndex = 1;

    public LayerMask collisionMask = -1;


    [Header("Movement")]

    public SpeedBlock normal = new SpeedBlock { moveSpeed = 10, rotateSpeed = 300 };
    public SpeedBlock boost = new SpeedBlock { moveSpeed = 20, rotateSpeed = 300 };
    public SpeedBlock fling = new SpeedBlock { moveSpeed = 30, rotateSpeed = 300 };
    public SpeedBlock flingAndBoost = new SpeedBlock { moveSpeed = 40, rotateSpeed = 300 };


    [Header("Timers")]
    
    [Tooltip("Duration of ability.")]
    public Timer boostTimer = new Timer(2);

    [Tooltip("Time between ability finished and able to be used again.")]
    public Timer boostCooldown = new Timer(4);

    [Tooltip("Duration of bonuses after releasing tether.")]
    public Timer flungTimer = new Timer(2);

    [Tooltip("Time between ability finished and able to be used again.")]
    public Timer flingCooldown = new Timer(0);

    [Tooltip("Time between ability finished and able to be used again.")]
    public Timer bombCooldown = new Timer(4);


	[Header("Bomb")]

	public GameObject bombPrefab;


	[Header("Other")]

	public float damage = 100;
    public AudioSource explosion;



	#endregion Public
	#region Private



	// Unity Event Methods

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
        healthEntity = GetComponent<HealthEntity>();
    }
	//~ fn

	void Update()
	{
        switch (BoostState)
        {
            case BoostState.None:
                {
                    // If player presses boost button,
                    if (GetBoostButton)
                    {
                        // Start boosting.
                        StartBoost();
                    }
                }
                break;
            case BoostState.Cooldown:
                {
                    // If cooldown is finished,
                    if (boostCooldown.UpdateTimer(Time.deltaTime))
                    {
                        // Ability is available.
                        BoostState = BoostState.None;
                    }
                }
                break;
            case BoostState.Boosting:
            default:
                break;
        }
        //~ switch
        
        switch (FlingState)
        {
            case FlingState.None:
                {
                    // If player presses tether button and a tether is in range,
                    if (GetTetherButton && m_TetherColliders.Count > 0)
                    {
                        StartFlinging();
                    }
                }
                break;
            case FlingState.Flinging:
                {
                    // If tether button is released,
                    if (!GetTetherButton)
                    {
                        // Stop flinging around pivot and start flying forwards.
                        FinishFlinging();
                    }
                }
                break;
            case FlingState.Cooldown:
                {
                    // If cooldown is finished,
                    if (flingCooldown.UpdateTimer(Time.deltaTime))
                    {
                        // Ability is available.
                        FinishFlingCooldown();
                    }
                }
                break;
            case FlingState.Flung:
            default:
                break;
        }
        //~ switch

        // DEBUG display input axes values
        //Debug.LogFormat(this,
        //	"P: {0}, H: {1}, V: {2}, F1: {3}, F2: {4}, F3: {5}",
        //	PlayerIndex, GetHorizontalAxis,
        //	GetVerticalAxis,
        //	GetBoostButton,
        //	GetTetherButton,
        //	GetBombButton
        //	);

		// Gravity Bomb
		if (GetBombButton && bombCooldown.IsFinished)
		{
            bombCooldown.ResetTimer();
			SpawnGravityBomb();
		}
    }
    //~ fn

    void FixedUpdate()
    {
        switch (BoostState)
        {
            case BoostState.Boosting:
                {
                    // If bonuses have run its course,
                    if (boostTimer.UpdateTimer(Time.fixedDeltaTime))
                    {
                        // Start cooldown.
                        FinishBoost();
                    }
                }
                break;
            case BoostState.Cooldown:
            case BoostState.None:
            default:
                break;
        }
        //~ switch

        switch (FlingState)
        {
            case FlingState.Flung:
                {
                    // If bonuses have run its course,
                    if (flungTimer.UpdateTimer(Time.deltaTime))
                    {
                        // Start cooldown.
                        FinishFlung();
                    }
                }
                break;
            case FlingState.Cooldown:
            case FlingState.Flinging:
            case FlingState.None:
            default:
                break;
        }
        //~ switch

        if (FlingState == FlingState.Flinging)
		{
			// Fling around the pivot at a fixed radius.
			Vector2 fromPivot = rb.position - (Vector2)FlingPivot.position;

            float speed = IsBoosting ? flingAndBoost.moveSpeed : fling.moveSpeed;
			
			float angle = Vector2Utilities.CalculateArcAngle(fromPivot.magnitude, speed * Time.fixedDeltaTime);
			if (IsFlingDirectionClockwise)
				angle = -angle;

			Vector2 rotatedVector = Vector2Utilities.Rotate(fromPivot, angle);
			Vector2 newPos = (Vector2)FlingPivot.position + rotatedVector;

            // Check if it would hit anything
            //RaycastHit hit;
            //Ray ray = new Ray(rb.position, newPos - rb.position);
            //if (GetComponent<CircleCollider2D>().Cast() Physics2D.Raycast(ray, out hit, ray.direction.magnitude, collisionMask, QueryTriggerInteraction.Ignore))
            //{
            //    Debug.Log("hit!");
            //    float radius = 0.5f;
            //    if (hit.distance > radius)
            //    {
            //        newPos = ray.GetPoint(hit.distance - radius);
            //    }
            //    else
            //    {
            //        newPos = rb.position;
            //    }
            //}

            // Move to new position
			rb.MovePosition(newPos);

			// Update rotation.
			Vector2 toPivot = (Vector2)FlingPivot.position - newPos;
			Vector2 direction = Vector2Utilities.Perpendicular(toPivot);
			if (IsFlingDirectionClockwise == false)
				direction = -direction;

			float newRotation = Quaternion.LookRotation(Vector3.forward, direction).eulerAngles.z;
			rb.MoveRotation(newRotation);

			rb.velocity = direction.normalized * speed;
			rb.angularVelocity = angle;
		}
		else
		{
			// Core Movement
            SpeedBlock movement;
			if (FlingState == FlingState.Flinging || FlingState == FlingState.Flung)
			{
                movement = IsBoosting ? flingAndBoost : fling;
            }
			else if (BoostState == BoostState.Boosting)
			{
                movement = boost;
            }
			else
			{
                movement = normal;
            }

            float move = GetVerticalAxis * movement.moveSpeed;
            float rot = -GetHorizontalAxis * movement.rotateSpeed;

            rb.velocity = transform.up * move;
			rb.angularVelocity = rot;
		}
		//~ else
	}
	//~ fn

	void OnCollisionEnter2D(Collision2D col)
    {
        EnemyScript enemy = col.gameObject.GetComponent<EnemyScript>();
		if (enemy && enemy.playerIndexToFollow != this.PlayerIndex)
		{
			enemy.GetComponent<HealthEntity>().Hurt(damage);

            // Play explosion
            explosion.Play();
		}
	}
	//~ fn

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == FlingPivotTag)
		{
            m_TetherColliders.Add(other);
		}
	}
    //~ fn

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == FlingPivotTag)
        {
            m_TetherColliders.Remove(other);
        }
    }
    //~ fn



    // Private Methods

    private void StartBoost()
    {
        BoostState = BoostState.Boosting;
        boostTimer.ResetTimer();
        healthEntity.Invincible = true;
    }
    //~ fn

    private void FinishBoost()
    {
        BoostState = BoostState.Cooldown;
        boostCooldown.ResetTimer();
        healthEntity.Invincible = false;
    }
    //~ fn

    private void FinishBoostCooldown()
    {
        BoostState = BoostState.None;
    }
    //~ fn


    private void StartFlinging()
    {
        if (m_TetherColliders.Count <= 0)
            throw new InvalidOperationException("Not close to a fling pivot.");

        // Start flinging around pivot.
        FlingState = FlingState.Flinging;
        rb.isKinematic = true;
        FlingPivot = m_TetherColliders
            .OrderBy(x => Vector3.Distance(x.transform.position, this.transform.position))
            .First()
            .transform;

        Vector2 toPivot = transform.position - FlingPivot.position;
        Vector2 clockwiseForwards = Vector2Utilities.Rotate(toPivot, 90);

        IsFlingDirectionClockwise = (Vector2.Dot(transform.up, clockwiseForwards) < 0);
    }
    //~ fn

    private void FinishFlinging()
	{
		FlingState = FlingState.Flung;
		rb.isKinematic = false;
		FlingPivot = null;
        flungTimer.ResetTimer();
	}
	//~ fn

    private void FinishFlung()
    {
        FlingState = FlingState.Cooldown;
        flingCooldown.ResetTimer();
    }
    //~ fn

    private void FinishFlingCooldown()
    {
        FlingState = FlingState.None;
    }
    //~ fn
    

    private void SpawnGravityBomb()
	{
        if (!bombPrefab)
            return;

        Vector3 pos = transform.position;
        pos.z += 10;
		Instantiate(bombPrefab, pos, transform.rotation);
	}
    //~ fn



    // Private Static Fields

    private static readonly PlayerShip[] s_Players = new PlayerShip[2];

    private HashSet<Collider2D> m_TetherColliders = new HashSet<Collider2D>();



    #endregion Private
}
//~ class
