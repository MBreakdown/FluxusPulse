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
using System.Collections;
using UnityEngine;

// Types

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
public class PlayerShip : MonoBehaviour
{
	#region Public



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



	// Public Auto Properties

	public Rigidbody2D rb { get; private set; }

	public BoostState BoostState { get; private set; } = BoostState.None;

	public FlingState FlingState { get; private set; } = FlingState.None;
	public Transform FlingPivot { get; private set; } = null;
	public bool IsFlingDirectionClockwise { get; private set; } = false;



	// Public Constants

	public const string FlingPivotTag = "FlingPivot";



	// Inspector Fields

	[Range(1, 2)]
	public int PlayerIndex = 1;


	[Header("Core Movement")]

	public float speed = 12f;
	public float rotateSpeed = 300f;


	[Header("Boost")]

	public float boostSpeed = 20f;
	public float boostRotateSpeed = 200f;
	public float boostTime = 2f;
	public float boostCooldown = 3f;


	[Header("Flinging")]

	public float flingingSpeed = 30f;
	public float flungSpeed = 30f;
	public float flungRotateSpeed = 100f;
	public float flungTime = 2f;
	public float flingingCooldown = 3f;


	[Header("Bomb")]

	public GameObject referenceBomb;
	public bool BombOffCooldown = true;


	[Header("Stuff")]

	public float damage;
	public float time = 0;



	#endregion Public
	#region Private



	// Unity Event Methods

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	//~ fn

	void Update()
	{
		// DEBUG display input axes values
		//Debug.LogFormat(this,
		//	"P: {0}, H: {1}, V: {2}, F1: {3}, F2: {4}, F3: {5}",
		//	PlayerIndex, GetHorizontalAxis,
		//	GetVerticalAxis,
		//	GetBoostButton,
		//	GetTetherButton,
		//	GetBombButton
		//	);

		// Boost Ability
		if (GetBoostButton
			&& BoostState == BoostState.None)
		{
			BoostState = BoostState.Boosting;
			StartCoroutine(BoostCoroutine());
		}

		// Flinging

		if (FlingState == FlingState.Flinging)
		{
			if (!GetTetherButton)
			{
				// Stop flinging around pivot and start flying forwards.
				FinishFlinging();
			}
		}

		// Gravity Bomb
		if (GetBombButton && BombOffCooldown)
		{
			// Set the cooldown to be on
			BombOffCooldown = false;

			Debug.Log("YOOOO");
			/*
			// Run function
			gravityBomb();
			*/
		}
	}
	//~ fn

	void FixedUpdate()
	{
		// Check the bomb's cooldown
		if (!BombOffCooldown)
		{
			// Use time
			time += Time.fixedDeltaTime;

			// Reactivate after 5 seconds
			if (time > 5)
			{
				// Set the bomb cooldown to active
				BombOffCooldown = true;

				// Reset the timer
				time = 0;
			}
		}
		
		if (FlingState == FlingState.Flinging)
		{
			// Fling around the pivot at a fixed radius.
			Vector2 fromPivot = transform.position - FlingPivot.position;
			
			float angle = Vector2Utilities.CalculateArcAngle(fromPivot.magnitude, flingingSpeed * Time.fixedDeltaTime);
			if (IsFlingDirectionClockwise)
				angle = -angle;

			Vector2 rotatedVector = Vector2Utilities.Rotate(fromPivot, angle);
			Vector2 newPos = (Vector2)FlingPivot.position + rotatedVector;
			rb.MovePosition(newPos);

			// Update rotation.
			Vector2 toPivot = (Vector2)FlingPivot.position - newPos;
			Vector2 direction = Vector2Utilities.Perpendicular(toPivot);
			if (IsFlingDirectionClockwise == false)
				direction = -direction;

			float newRotation = Quaternion.LookRotation(Vector3.forward, direction).eulerAngles.z;
			rb.MoveRotation(newRotation);

			rb.velocity = direction.normalized * flingingSpeed;
			rb.angularVelocity = angle;
		}
		else
		{
			// Core Movement
			float move = GetVerticalAxis;
			float rot = -GetHorizontalAxis;

			if (IsFlung)
			{
				move *= flungSpeed;
				rot *= flungRotateSpeed;
			}
			else if (IsBoosting)
			{
				move *= boostSpeed;
				rot *= boostRotateSpeed;
			}
			else
			{
				move *= speed;
				rot *= rotateSpeed;
			}
			
			rb.velocity = transform.up * move;
			rb.angularVelocity = rot;
		}
		//~ else
	}
	//~ fn

	void OnCollisionEnter2D(Collision2D col)
	{
		EnemyScript enemy = col.gameObject.GetComponent<EnemyScript>();
		if (enemy && enemy.playerToAvoid == this.transform)
		{
			Debug.Log("Hurt() has been called");
			enemy.GetComponent<HealthEntity>().Hurt(damage);
		}
	}
	//~ fn

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == FlingPivotTag)
		{
			if (GetTetherButton
				&& FlingState == FlingState.None)
			{
				// Start flinging around pivot.
				FlingState = FlingState.Flinging;
				rb.isKinematic = true;
				FlingPivot = other.transform;

				Vector2 toPivot = transform.position - FlingPivot.position;
				Vector2 clockwiseForwards = Vector2Utilities.Rotate(toPivot, 90);

				IsFlingDirectionClockwise = (Vector2.Dot(transform.up, clockwiseForwards) < 0);
			}
		}
	}
	//~ fn

	void OnTriggerExit2D(Collider2D other)
	{
		if (FlingState == FlingState.Flinging)
		{
			FinishFlinging();
		}
	}
	//~ fn



	// Private Methods

	private void FinishFlinging()
	{
		FlingState = FlingState.Flung;
		rb.isKinematic = false;
		FlingPivot = null;
		StartCoroutine(FlungCoroutine());
	}
	//~ fn

	/*private void gravityBomb()
	{
		// Fire bullet
		GameObject bomb = Instantiate(referenceBomb, new Vector2(transform.position.x, transform.position.y), Quaternion.LookRotation(Vector3.forward, Vector3.forward));
	}*/

	// Coroutines

	private IEnumerator BoostCoroutine()
	{
		yield return new WaitForSeconds(boostTime);
		BoostState = BoostState.Cooldown;

		yield return new WaitForSeconds(boostCooldown);
		BoostState = BoostState.None;
	}
	//~ fn

	private IEnumerator FlungCoroutine()
	{
		yield return new WaitForSeconds(flungTime);
		FlingState = FlingState.Cooldown;

		yield return new WaitForSeconds(flingingCooldown);
		FlingState = FlingState.None;
	}
	//~ fn



	#endregion Private
}
//~ class
