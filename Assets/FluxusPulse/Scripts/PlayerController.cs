using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed = 10.0f;
	public string inputHorizontal = "Horizontal";
	public string inputVertical = "Vertical";



	// Use this for initialization before the first Update.
	void Start()
	{
		
	}
	
	// Update is called once per frame.
	void Update()
	{
		// Get mouse position in screen space.
		Vector2 mousePos = Input.mousePosition;
		// Convert to world space.
		Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

		// Get current position in world space.
		Vector2 playerPos = this.transform.position;

		// Get vector looking from the player to the mouse.
		Vector2 lookDirection = worldMousePos - playerPos;

		// Set rotation so the player so their local Y axis points towards the mouse.
		this.transform.rotation = Quaternion.LookRotation (Vector3.forward, lookDirection);

		// Rotate by 90 degrees so their local X axis points towards the mouse.
		this.transform.Rotate(0, 0, 90f);
	}

	// FixedUpdate is called once per physics cycle.
	void FixedUpdate()
	{
		// Get the Rigidbody2D component of this GameObject.
		// It may be null or destroyed.
		var rb = this.GetComponent<Rigidbody2D> ();

		// If the component exists and is not destroyed,
		if (rb) {
			
			// Get horizontal input in the range [-1..1]
			float x = Input.GetAxis (inputHorizontal);

			// Get vertical input in the range [-1..1]
			float y = Input.GetAxis (inputVertical);

			// Get the direction of desired movement.
			Vector2 dir = new Vector2(x, y);

			// Limit it so you cannot forward strafe faster.
			dir = Vector2.ClampMagnitude(dir, 1f);

			// Set the player's velocity manually.
			rb.velocity = dir * speed;
		}
	}
}
