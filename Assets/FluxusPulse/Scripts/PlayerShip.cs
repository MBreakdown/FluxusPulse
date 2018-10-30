using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoostState
{
    None,

    // Increased speed for a short time.
    Boosting,

    // Waiting until it can be used again.
    Cooldown,
}

public enum FlingState
{
    None,

    // Rotating around a pivot point with increased speed.
    Flinging,

    // Flying forwards with high speed.
    Flung,

    // Cooldown after flinging.
    Cooldown,
}

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerShip : MonoBehaviour
{
    // Inspector Fields

    public InputScheme input;

    [Header("Core Movement")]

    public float speed = 12f;
    public float rotateSpeed = 300f;

    public Rigidbody2D rb { get; private set; }


    [Header("Boost")]

    public float boostSpeed = 20f;
    public float boostRotateSpeed = 200f;
    public float boostTime = 2f;
    public float boostCooldown = 3f;
    public BoostState BoostState { get; private set; } = BoostState.None;

    public bool IsBoosting => BoostState == BoostState.Boosting;


    [Header("Flinging")]

    public float flingingSpeed = 30f;
    public float flungSpeed = 30f;
    public float flungRotateSpeed = 100f;
    public float flungTime = 2f;
    public float flingingCooldown = 3f;

    public const string FlingPivotTag = "FlingPivot";

    public FlingState FlingState { get; private set; } = FlingState.None;
    public Transform FlingPivot { get; private set; } = null;
    public bool IsFlingDirectionClockwise { get; private set; } = false;

    public bool IsFlinging => FlingState == FlingState.Flinging;
    public bool IsFlung => FlingState == FlingState.Flung;

    [Header("Stats")]

    public float damage;

    // Unity Events

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Boost Ability
        if (input.BoostButton
            && BoostState == BoostState.None)
        {
            BoostState = BoostState.Boosting;
            StartCoroutine(BoostCoroutine());
        }

        // Flinging

        if (FlingState == FlingState.Flinging)
        {
            if (!input.FlingButton)
            {
                // Stop flinging around pivot and start flying forwards.
                FinishFlinging();
            }
        }
    }

    void FixedUpdate()
    {
        if (FlingState == FlingState.Flinging)
        {
            // Fling around the pivot at a fixed radius.
            Vector2 fromPivot = transform.position - FlingPivot.position;
            
            float angle = Vector2Utilities.CalculateArcAngle(fromPivot.magnitude, flingingSpeed * Time.fixedDeltaTime);
            if (IsFlingDirectionClockwise)
                angle = -angle;

            Vector2 rotatedVector = Vector2Utilities.Rotate(fromPivot, angle);
            Vector2 newPos = (Vector2)FlingPivot.position + rotatedVector;
            rb.MovePosition(newPos);

            // Update rotation.
            Vector2 toPivot = (Vector2)FlingPivot.position - newPos;
            Vector2 direction = Vector2Utilities.Perpendicular(toPivot);
            if (IsFlingDirectionClockwise == false)
                direction = -direction;

            float newRotation = Quaternion.LookRotation(Vector3.forward, direction).eulerAngles.z;
            rb.MoveRotation(newRotation);

            rb.velocity = direction.normalized * flingingSpeed;
            rb.angularVelocity = angle;
        }
        else
        {
            // Core Movement
            float move = input.VerticalAxis;
            float rot = -input.HorizontalAxis;

            if (IsFlung)
            {
                move *= flungSpeed;
                rot *= flungRotateSpeed;
            }
            else if (IsBoosting)
            {
                move *= boostSpeed;
                rot *= boostRotateSpeed;
            }
            else
            {
                move *= speed;
                rot *= rotateSpeed;
            }
            
            rb.velocity = transform.up * move;
            rb.angularVelocity = rot;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        EnemyScript enemy = col.gameObject.GetComponent<EnemyScript>();
        if (enemy && enemy.playerToAvoid == this)
        {
            enemy.GetComponent<HealthEntity>().Hurt(damage);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == FlingPivotTag)
        {
            if (input.FlingButton
                && FlingState == FlingState.None)
            {
                // Start flinging around pivot.
                FlingState = FlingState.Flinging;
                rb.isKinematic = true;
                FlingPivot = other.transform;

                Vector2 toPivot = transform.position - FlingPivot.position;
                Vector2 clockwiseForwards = Vector2Utilities.Rotate(toPivot, 90);

                IsFlingDirectionClockwise = (Vector2.Dot(transform.up, clockwiseForwards) < 0);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (FlingState == FlingState.Flinging)
        {
            FinishFlinging();
        }
    }

    void FinishFlinging()
    {
        FlingState = FlingState.Flung;
        rb.isKinematic = false;
        FlingPivot = null;
        StartCoroutine(FlungCoroutine());
    }


    // Coroutines

    private IEnumerator BoostCoroutine()
    {
        yield return new WaitForSeconds(boostTime);
        BoostState = BoostState.Cooldown;

        yield return new WaitForSeconds(boostCooldown);
        BoostState = BoostState.None;
    }

    private IEnumerator FlungCoroutine()
    {
        yield return new WaitForSeconds(flungTime);
        FlingState = FlingState.Cooldown;

        yield return new WaitForSeconds(flingingCooldown);
        FlingState = FlingState.None;
    }
}
