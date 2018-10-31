using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour {

    public float speed;
    public float damage;
    public GameObject playerFired;

    // Use this for initialization
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.up * speed;
    }
	
    // Fixed update is called once every physics frame
    void FixedUpate()
    {

    }

	// Update is called once per frame
	void Update()
    {
		
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<PlayerShip>() != null)
        {
            // Damage player
            col.gameObject.GetComponent<HealthEntity>().Hurt(damage);

            // Self destruct
            Destroy(this.gameObject);
        }
        else if (col.gameObject.GetComponent<EnemyScript>().gameObject == playerFired)
        {
            // Do nothing if collision with an enemy or bullet
        }
        else
        {
            // Self destruct
            Destroy(this.gameObject);
        }
    }
}
