using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(ChargedEntity))]
[RequireComponent(typeof(GravityWell))]
[DisallowMultipleComponent]
public class PlayerController : MonoBehaviour
{
    // Inspector Fields

    [Header("Movement")]

    public float speed = 3f;
    public float angularSpeed = 20f;
    
    public float maxSpeed = 10f;
	public float maxAngularSpeed = 180f;


	[Header("Input Axes")]

	public string inputHorizontal = "Horizontal";

	public string inputVertical = "Vertical";

	public string inputSwitchPolarity = "Fire2";


	// Private Fields

	public Rigidbody2D rb { get; private set; }

	public ChargedEntity chargedEntity { get; private set; }


	// Unity Event Methods

	void Awake()
	{
		// Get the Rigidbody2D component of this GameObject.
		rb = GetComponent<Rigidbody2D>();

		// Get the PolarEntity component of this GameObject.
		chargedEntity = GetComponent<ChargedEntity>();
	}


	void Update()
	{
		// If player pressed the button this frame,
		if (Input.GetButtonDown(inputSwitchPolarity))
		{
			chargedEntity.InvertCharge();
		}

        // If player is holding button down, activate polarity.
        float input = Input.GetAxisRaw(inputVertical);
        bool tryingToMove = Mathf.Abs(input) > 0.01f;
        chargedEntity.enabled = tryingToMove;
	}


	void FixedUpdate()
	{
        MovementUpdate(Input.GetAxisRaw(inputVertical));
		RotationUpdate(-Input.GetAxisRaw(inputHorizontal));
	}

    private void MovementUpdate(float input)
    {
        bool tryingToMove = Mathf.Abs(input) > 0.01f;
        if (tryingToMove)
        {
            rb.AddForce(transform.up * speed * input);
        }
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
    }

	private void RotationUpdate(float input)
	{
		bool tryingToRotate = Mathf.Abs(input) > 0.01f;
		if (tryingToRotate)
		{
            rb.AddTorque(angularSpeed * input);
        }
        rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, -maxAngularSpeed, maxAngularSpeed);
    }
}