using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Reference variable, assignable in the inspector.
    public Transform playerToFollow;
    public Transform playerToAvoid;
    public float speed = 10;
    public float maxSpeed = 20;
    public float health = 100;

	// Use this for initialization
	void Start()
    {
        Debug.Log(playerToFollow.name);
	}

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

        Vector2 vectorToPlayer = playerToFollow.transform.position - this.transform.position;
        rb.AddForce(vectorToPlayer.normalized * speed);

        // Ensure max speed
        if (rb.velocity.sqrMagnitude > (maxSpeed * maxSpeed))
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }

        // Rotate towards the correct direction
        rb.rotation = Mathf.Rad2Deg * Mathf.Atan2(vectorToPlayer.y, vectorToPlayer.x) + 90;
    }

    // Update is called once per frame.
    // Use Time.deltaTime here.
    // Use game input here.
    void Update()
    {

    }

    // Run collisions
    void OnCollisionEnter2D(Collision2D col)
    {
        // Run collision with player to follow
        if (col.gameObject.name == playerToFollow.name)
        {
            // Damage player
            Destroy(col.gameObject);

            // Self destruct
            Destroy(this.gameObject);
        }
        // Placeholder TODO remove
        if (col.gameObject.name == playerToAvoid.name)
        {
            Destroy(this.gameObject);
        }
    }
}