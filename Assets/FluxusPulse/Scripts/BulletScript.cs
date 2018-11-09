/***********************************************************************
*	Bachelor of Software Engineering
*	Media Design School
*	Auckland
*	New Zealand
*
*	(c) 2018 Media Design School
*
*	File Name	:	BulletScript.cs
*	Description	:	Projectile with linear trajectory that can deal damage to player ships.
*	Project		:	FluxusPulse
*	Team Name	:	M Breakdown Studios
***********************************************************************/
using UnityEngine;

public class BulletScript : MonoBehaviour
{
	#region Public



	// Inspector Fields

	public float speed = 1;
	public float damage = 5;
    public AudioSource hurt;

    [HideInInspector]
	public EnemyScript originEnemy;



	#endregion Public
	#region Private



	// Unity Event Methods

	// Use this for initialization
	void Start()
	{
		Rigidbody2D rb = GetComponent<Rigidbody2D>();

		rb.velocity = transform.up * speed;

        Destroy(gameObject, 30f);
	}
	//~ fn

	void OnTriggerEnter2D(Collider2D col)
	{
        if (col.isTrigger)
            return;

        PlayerShip player = col.GetComponent<PlayerShip>();
        if (player != null)
        {
            // Play hurt sound
            FindObjectOfType<GameController>().hurt.Play();

            // Damage player
            player.healthEntity.Hurt(damage);

            // Self destruct
            Destroy(gameObject);
        }
        else
        {
            EnemyScript enemy = col.GetComponent<EnemyScript>();
            if (enemy == originEnemy)
            {
                // Do nothing if collision with the enemy that fired it
            }
            else
            {
                // Self destruct
                Destroy(gameObject);
            }
        }
	}
	//~ fn
    


	#endregion Private
}
//~ class
