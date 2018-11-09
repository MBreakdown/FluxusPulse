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

public class KeepRotationZero : MonoBehaviour
{
    void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }
}
//~ class
