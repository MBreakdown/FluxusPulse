using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipThrusterFx : MonoBehaviour
{
    // Inspector Fields

    public InputScheme input;
    public PlayerShip player;
    public ParticleSystem thrusterFx;
    public ParticleSystem boostFx;
    public ParticleSystem highSpeedFx;

    [Tooltip("Minimum speed to show high speed fx.")]
    public float highSpeedThreshold = 5f;



    // Unity Events

    private void Update()
    {
        // Decide which effects should be active.

        bool enableThruster;
        bool enableBoost;
        bool enableHighSpeed;


        if (Mathf.Abs(input.VerticalAxis) > 0.01f)
        {
            enableThruster = true;

            if (player.IsBoosting)
            {
                enableBoost = true;
            }
            else
            {
                enableBoost = false;
            }
        }
        else
        {
            enableThruster = false;
            enableBoost = false;
        }


        if (player.rb.velocity.magnitude >= highSpeedThreshold)
        {
            enableHighSpeed = true;
        }
        else
        {
            enableHighSpeed = false;
        }


        // Apply changes

        var thrusterEmission = thrusterFx.emission;
        if (thrusterEmission.enabled != enableThruster)
        {
            thrusterEmission.enabled = enableThruster;
        }

        var boostEmission = boostFx.emission;
        if (boostEmission.enabled != enableBoost)
        {
            boostEmission.enabled = enableBoost;
        }

        var highSpeedEmission = highSpeedFx.emission;
        if (highSpeedEmission.enabled != enableHighSpeed)
        {
            highSpeedEmission.enabled = enableHighSpeed;
        }
    }
}
