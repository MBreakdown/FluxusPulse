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

public class MineScript : MonoBehaviour
{
    #region Public



    // Inspector Fields
    
    public float damage = 5;
    public GameObject playerToHurt;

	[Header("Effects")]
	public GameObject pinkPart;
	public GameObject redPart;
	public GameObject greenPart;

    #endregion Public
    #region Private



    // Unity Event Methods

    // Use this for initialization
    void Start()
    {

    }
    //~ fn

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger)
            return;

        PlayerShip player = col.GetComponent<PlayerShip>();
        if (player != null)
        {
            if (player == playerToHurt.GetComponent<PlayerShip>())
            {
                // Damage player
                player.healthEntity.Hurt(damage);
                
                // Don't destroy mines if invincible
                if (player.GetComponent<HealthEntity>().Invincible)
                {
                    return;
                }
            }

            // Self destruct
			Instantiate (redPart, this.transform.position, this.transform.rotation);
            Destroy(gameObject);
        }
    }
    //~ fn



    #endregion Private
}
//~ class
