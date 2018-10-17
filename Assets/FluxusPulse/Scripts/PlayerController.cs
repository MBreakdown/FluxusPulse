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

	//[Tooltip("Metres per second.")]
	//public float maxSpeed = 10f;

	[Tooltip("Degrees per second.")]
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
		chargedEntity.enabled = Input.GetAxisRaw(inputVertical) > 0.01f;
	}


	void FixedUpdate()
	{
		RotationUpdate(-Input.GetAxisRaw(inputHorizontal));
	}


	private void RotationUpdate(float input)
	{
		bool tryingToRotate = Mathf.Abs(input) > 0.01f;
		if (tryingToRotate)
		{
			rb.angularVelocity = maxAngularSpeed * input;
		}
    }
}
