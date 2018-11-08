/***********************************************************************
*	Bachelor of Software Engineering
*	Media Design School
*	Auckland
*	New Zealand
*
*	(c) 2018 Media Design School
*
*	File Name	:	PlayerHud.cs
*	Description	:	Heads-up display for a player.
*	Project		:	FluxusPulse
*	Team Name	:	M Breakdown Studios
***********************************************************************/
using UnityEngine;

public class PlayerHud : MonoBehaviour
{
    // Public Properties

    public PlayerShip Player { get; private set; }



    // Inspector Fields

    public 



    // Unity Event Methods

    void Awake()
    {
        Player = GetComponentInParent<PlayerShip>();
        if (!Player)
            Debug.LogError("Player not found.");
    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }
}
//~ class
