using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[DisallowMultipleComponent]
public class ChargedEntity : MonoBehaviour
{
	// Inspector Fields

	[Tooltip("Greater than zero for positive charge.\n"
		+ "Less than zero for negative charge.\n"
		+ "Zero for no charge.")]
	[SerializeField]
	private float m_charge = 10f;


	[Tooltip("Charge multiplier with respect to an angle from forwards.")]
	[SerializeField]
	private AnimationCurve chargeWrtAngle = new AnimationCurve(
		new Keyframe(0f, 1f),
		new Keyframe(180f, 1f)
		);


	// Private Fields
	
	public Rigidbody2D rb { get; private set; }


	// Properties

	private float charge {
		get { return m_charge; }
		set {
			m_charge = value;
			BroadcastMessage("OnChargeChanged", charge, SendMessageOptions.DontRequireReceiver);
		}
	}

	public float GetCharge(float angleFromForwards)
	{
		return charge * chargeWrtAngle.Evaluate(angleFromForwards);
	}

	public float GetCharge(Vector2 direction)
	{
		return GetCharge(Vector2.Angle(transform.up, direction));
	}

	
	// Methods

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		// initialize property
		charge = charge;
	}
	
	public void InvertCharge()
	{
		charge = -charge;
	}
}
