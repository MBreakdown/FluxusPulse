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

public class GravityBombScript : MonoBehaviour
{
    #region Public

    // Inspector Fields

    public float speed;
    public float damage;

    #endregion Public

    #region Private

    // Unity Event Methods

    // Use this for initialization
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.up * speed;
    }
    //~ fn

    // Fixed update is called once every physics frame
    void FixedUpdate()
    {

    }
    //~ fn

    // Update is called once per frame
    void Update()
    {

    }
    //~ fn

    #endregion Private
}
//~ class
