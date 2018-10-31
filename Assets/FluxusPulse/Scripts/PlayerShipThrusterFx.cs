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



	// Inspector Fields

	public PlayerShip player;
	public InputScheme input;
	public SpeedFx[] speedFxGroups;
	public ParticleSystem boostFx;
	public ParticleSystem flingFx;



	#endregion Public
	#region Private



	// Unity Event Methods

	private void Update()
	{
		// Decide which effects should be active.
		bool enableBoost = player.IsBoosting;
		var boostEmission = boostFx.emission;
		if (boostEmission.enabled != enableBoost)
		{
			boostEmission.enabled = enableBoost;
		}

		bool enableFling = player.IsFlinging || player.IsFlung;
		var flingEmission = flingFx.emission;
		if (flingEmission.enabled != enableFling)
		{
			flingEmission.enabled = enableFling;
		}

		// Speed Fx Groups
		float speed = player.rb.velocity.magnitude;
		foreach (var speedFx in speedFxGroups)
		{
			bool enableFx = (speed >= speedFx.speedThresholdMin
				&& speed <= speedFx.speedThresholdMax);
			
			var emission = speedFx.fx.emission;
			if (emission.enabled != enableFx)
			{
				emission.enabled = enableFx;
			}
		}
	}
	//~ fn



	#endregion Private
}
//~ class
