using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Polarity
{
    Positive,
    Negative,
}

[RequireComponent(typeof(Rigidbody2D))]
[DisallowMultipleComponent]
public class PlayerController : MonoBehaviour
{
    // Inspector Fields

    public Polarity polarity = Polarity.Positive;

    [Tooltip("Metres per second.")]
    public float maxSpeed = 10f;

    [Tooltip("Degrees per second.")]
    public float maxAngularSpeed = 180f;

    [Tooltip("Metres per second per second.")]
    public float acceleration = 10f;

    [Tooltip("Metres per second per second.")]
    public float deceleration = 20f;

    [Tooltip("Degrees per second per second.")]
    public float angularAcceleration = 100f;

    [Tooltip("Degrees per second per second.")]
    public float angularDeceleration = 200f;
    
    public string inputHorizontal = "Horizontal";

    public string inputVertical = "Vertical";


    // Private Fields

    private Rigidbody2D rb;


    // Methods
    
    void Awake()
    {
        // Get the Rigidbody2D component of this GameObject.
        rb = this.GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        if (!rb)
            return;

        RotationUpdate();
        MovementUpdate();
    }

    void MovementUpdate()
    {
        float vert = Input.GetAxisRaw(inputVertical);
        bool tryingToMove = (Mathf.Abs(vert) > 0.01f);

        // desired Y (forwards) velocity in local space
        float desiredRelVelY = (tryingToMove ?
            maxSpeed * Mathf.Sign(vert) :
            0f);

        Vector2 currentRelVel = transform.InverseTransformDirection(rb.velocity);

        float accelY = ((tryingToMove && Mathf.Sign(desiredRelVelY) == Mathf.Sign(currentRelVel.y)) ?
            acceleration :
            deceleration);

        float newRelVelY = Mathf.MoveTowards(
            currentRelVel.y,
            desiredRelVelY,
            accelY * Time.fixedDeltaTime);

        // desired X velocity in local space
        float desiredRelVelX = 0f;
        float accelX = deceleration;

        float newRelVelX = Mathf.MoveTowards(
            currentRelVel.x,
            desiredRelVelX,
            accelX * Time.fixedDeltaTime);

        rb.velocity = transform.TransformDirection(new Vector2(newRelVelX, newRelVelY));
    }

    void RotationUpdate()
    {
        float horz = -Input.GetAxisRaw(inputHorizontal);
        bool tryingToRotate = Mathf.Abs(horz) > 0.01f;

        // desired angular velocity
        float desiredAngVel = (tryingToRotate ?
            maxAngularSpeed * Mathf.Sign(horz) :
            0f);

        float currentAngVel = rb.angularVelocity;
        
        float accel = ((tryingToRotate && Mathf.Sign(desiredAngVel) == Mathf.Sign(currentAngVel)) ?
            angularAcceleration :
            angularDeceleration);

        float newAngVel = Mathf.MoveTowards(
            currentAngVel,
            desiredAngVel,
            accel * Time.fixedDeltaTime);

        rb.angularVelocity = newAngVel;
    }
}
