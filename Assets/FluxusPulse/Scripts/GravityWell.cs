using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Requires a trigger Collider2D on this GameObject or a child.
[DisallowMultipleComponent]
public class GravityWell : MonoBehaviour
{
	// Inspector Fields

	public ChargedEntity thisEntity;

	//public float force = 1f;

	//[Tooltip("Force with respect to distance from other entity.")]
	//public AnimationCurve forceWrtDistance = new AnimationCurve(
	//	new Keyframe(0f, 1f),
	//	new Keyframe(10f, 0f)
	//);

	//[Tooltip("Force with respect to angle from forwards to other entity.")]
	//public AnimationCurve forceWrtDirection = new AnimationCurve(
	//	new Keyframe(0f, 1f),
	//	new Keyframe(180f, 1f)
	//);


	// Private Fields

	// Set of all charged entities within this gravity well's area of effect.
	private HashSet<Rigidbody2D> entitiesInAoe = new HashSet<Rigidbody2D>();



	// Methods

	private void Awake()
	{
		if (!thisEntity)
			Debug.LogError("This Entity not assigned.", this);
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		// Add charged entity to set of entities in AOE.
		entitiesInAoe.Add(collision.attachedRigidbody);
	}


	private void OnTriggerExit2D(Collider2D collision)
	{
		// Remove charged entity from set of entities in AOE.
		entitiesInAoe.Remove(collision.attachedRigidbody);
	}

	
	void FixedUpdate()
	{
		if (!(thisEntity && thisEntity.isActiveAndEnabled))
			return;

		// For each active charged entity in the area of effect,
		foreach (var otherEntity in entitiesInAoe
			.Where(rb => (rb))
			.Select(rb => rb.GetComponent<ChargedEntity>())
			.Where(otherEntity => (otherEntity)
				&& otherEntity.isActiveAndEnabled
			))
		{
			// Vector from this object to the entity.
			Vector2 direction = otherEntity.rb.position - thisEntity.rb.position;

			if (direction.sqrMagnitude < 0.001f)
				continue;

			float thisCharge = thisEntity.GetCharge(direction);
			float otherCharge = otherEntity.GetCharge(-direction);

			if (!ChargeComparisons.IsInfluenced(thisCharge, otherCharge))
				continue;

			//const float coulombsConstant = 8.987552e9f;
			const float coulombsConstant = 10f;

			float force = ((thisCharge * otherCharge)
				/ direction.sqrMagnitude)
				* coulombsConstant;

			Vector2 forceVec = direction.normalized * force;
			otherEntity.rb.AddForce(forceVec);
			//thisEntity.rb.AddForce(-forceVec);
		}
	}
}
