using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovmentPlatform : MonoBehaviour {

	public bool ismovingleft = false;
	public float speed = 1;

	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.tag == "Platform") 
		{
			ismovingleft = !ismovingleft;
		}

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		if (ismovingleft) {
			rb.velocity = new Vector2 (-speed, 0);	
		}
		else {
			rb.velocity = new Vector2 (speed, 0);
		}
	}
}
