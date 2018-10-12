using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Polarity
{
	Positive,
	Negative,
}

[RequireComponent(typeof(Rigidbody2D))]
[DisallowMultipleComponent]
public class PlayerController : MonoBehaviour
{
	// Inspector Fields

	[Header("Polarity")]

	public Polarity polarity = Polarity.Positive;

	[Tooltip("Sprite renderer. Child of this GameObject.")]
	public SpriteRenderer spriteRenderer;

	[Tooltip("Sprite asset to use for positive polarity.")]
	public Sprite spritePositive;

	[Tooltip("Sprite asset to use for negative polarity.")]
	public Sprite spriteNegative;

	[Tooltip("Layers that can be attracted when positive.")]
	public LayerMask magnetMaskPositive = int.MaxValue;

	[Tooltip("Layers that can be attracted when negative.")]
	public LayerMask magnetMaskNegative = int.MaxValue;


	[Header("Movement")]

	[Tooltip("Metres per second.")]
	public float maxSpeed = 10f;

	// TODO maxSpeedWhenInRangeOfWall

	[Tooltip("Degrees per second.")]
	public float maxAngularSpeed = 180f;

	[Tooltip("Metres per second per second.")]
	public float acceleration = 10f;

	[Tooltip("Metres per second per second.")]
	public float deceleration = 20f;

	[Tooltip("Degrees per second per second.")]
	public float angularAcceleration = 100f;

	[Tooltip("Degrees per second per second.")]
	public float angularDeceleration = 200f;


	[Header("Input Axes")]

	public string inputHorizontal = "Horizontal";

	public string inputVertical = "Vertical";

	public string inputSwitchPolarity = "Fire1";


	// Private Fields

	private Rigidbody2D rb;


	// Properties

	public void SetPolarity(Polarity newPolarity)
	{
		this.polarity = newPolarity;

		if (spriteRenderer)
		{
			Sprite newSprite = newPolarity == Polarity.Positive ?
				spritePositive :
				spriteNegative;

			if (newSprite)
			{
				spriteRenderer.sprite = newSprite;
			}
		}
	}


	// Unity Event Methods

	void Awake()
	{
		// Get the Rigidbody2D component of this GameObject.
		rb = this.GetComponent<Rigidbody2D>();


		// Check that resource references are not null.

		if (!spriteRenderer)
		{
			Debug.LogError("Sprite Renderer not assigned.", this);
		}

		if (!spritePositive)
		{
			Debug.LogError("Sprite Positive not assigned.", this);
		}

		if (!spriteNegative)
		{
			Debug.LogError("Sprite Negative not assigned.", this);
		}
	}


	void Start()
	{
		// Initialize property.
		SetPolarity(this.polarity);
	}


	void Update()
	{
		// If player pressed the button this frame,
		if (Input.GetButtonDown(inputSwitchPolarity))
		{
			SwitchPolarity();
		}
	}


	void FixedUpdate()
	{
		if (!rb)
			return;

		RotationUpdate(-Input.GetAxisRaw(inputHorizontal));
		
		if (Physics2D.Raycast(
			transform.position,
			transform.up,
			1000,
			(polarity == Polarity.Negative ? magnetMaskNegative : magnetMaskPositive)
			))
		{
			MovementUpdate(Input.GetAxisRaw(inputVertical));
		}
		else
		{
			MovementUpdate(Input.GetAxisRaw(inputVertical) * 0.1f);
		}
	}


	// Methods

	private void MovementUpdate(float input)
	{
		bool tryingToMove = Mathf.Abs(input) > 0.01f;

		// desired Y (forwards) velocity in local space
		float desiredRelVelY = (tryingToMove ?
			maxSpeed * Mathf.Sign(input) :
			0f);

		Vector2 currentRelVel = transform.InverseTransformDirection(rb.velocity);

		float accelY = ((tryingToMove && Mathf.Sign(desiredRelVelY) == Mathf.Sign(currentRelVel.y)) ?
			acceleration :
			deceleration);

		float newRelVelY = Mathf.MoveTowards(
			currentRelVel.y,
			desiredRelVelY,
			accelY * Time.fixedDeltaTime);

		// desired X velocity in local space
		float desiredRelVelX = 0f;
		float accelX = deceleration;

		float newRelVelX = Mathf.MoveTowards(
			currentRelVel.x,
			desiredRelVelX,
			accelX * Time.fixedDeltaTime);

		rb.velocity = transform.TransformDirection(new Vector2(newRelVelX, newRelVelY));
	}


	private void RotationUpdate(float input)
	{
		bool tryingToRotate = Mathf.Abs(input) > 0.01f;

		// desired angular velocity
		float desiredAngVel = (tryingToRotate ?
			maxAngularSpeed * Mathf.Sign(input) :
			0f);

		float currentAngVel = rb.angularVelocity;

		float accel = ((tryingToRotate && Mathf.Sign(desiredAngVel) == Mathf.Sign(currentAngVel)) ?
			angularAcceleration :
			angularDeceleration);

		float newAngVel = Mathf.MoveTowards(
			currentAngVel,
			desiredAngVel,
			accel * Time.fixedDeltaTime);

		rb.angularVelocity = newAngVel;
	}


	private void SwitchPolarity()
	{
		// Toggle the polarity.
		switch (polarity)
		{
			case Polarity.Positive:
				{
					SetPolarity(Polarity.Negative);
				}
				break;
			case Polarity.Negative:
				{
					SetPolarity(Polarity.Positive);
				}
				break;
		}
	}
}
