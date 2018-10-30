using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour {

    public float speed;
    public float damage;

	// Use this for initialization
	void Start()
    {
        
    }
	
    // Fixed update is called once every physics frame
    void FixedUpate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Set velocity
        rb.velocity = transform.up * speed;
    }

	// Update is called once per frame
	void Update()
    {
		
	}

    void OnCollisionEnter(Collision2D col)
    {
        if (col.gameObject.GetComponent<PlayerShip>() != null)
        {
            // Damage player
            col.gameObject.GetComponent<HealthEntity>().Hurt(damage);
        }

        // Self destruct
        Destroy(this.gameObject);
    }
}
