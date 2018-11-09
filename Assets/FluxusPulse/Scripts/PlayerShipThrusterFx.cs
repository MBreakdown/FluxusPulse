/***********************************************************************
*	Bachelor of Software Engineering
*	Media Design School
*	Auckland
*	New Zealand
*
*	(c) 2018 Media Design School
*
*	File Name	:	PlayerShipThrusterFx.cs
*	Description	:	Manages the particle systems of the player's ship.
*	Project		:	FluxusPulse
*	Team Name	:	M Breakdown Studios
*	Author		:	Elijah Shadbolt
*	Mail		:	elijah.sha7979@mediadesign.school.nz
***********************************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipThrusterFx : MonoBehaviour
{
	#region Public



	// Types

	[Serializable]
	public class SpeedFx
	{
		public float speedThresholdMin = 10;
		public float speedThresholdMax = 1000;
		public ParticleSystem fx;
	}
    //~ class



	// Inspector Fields

	public PlayerShip player;
    public float speedThreshold = 3;
    public ParticleSystem[] movefx;
    public ParticleSystem[] boostfx;
    public ParticleSystem[] flingfx;



	#endregion Public
	#region Private



	// Unity Event Methods

    private void Awake()
    {
        if (!player)
        {
            player = GetComponentInParent<PlayerShip>();
            if (!player)
                Debug.LogError("Could not find player in hierarchy.", this);
        }
    }
    //~ fn

	private void Update()
    {
        SetEmmisionEnabled(boostfx, player.IsBoosting);
        SetEmmisionEnabled(flingfx, player.IsFlingingOrFlung);
        SetEmmisionEnabled(movefx, player.rb.velocity.sqrMagnitude > speedThreshold * speedThreshold);
	}
	//~ fn



    // Private Methods

    void SetEmmisionEnabled(IEnumerable<ParticleSystem> fx, bool enable)
    {
        foreach (ParticleSystem ps in fx)
        {
            if (!ps)
                continue;

            var emission = ps.emission;
            if (emission.enabled != enable)
            {
                emission.enabled = enable;
            }
        }
    }
    //~ fn



	#endregion Private
}
//~ class
