using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[DisallowMultipleComponent]
[System.Obsolete("Obsolete.")]
public class PlayerRenderer : MonoBehaviour
{
	// Inspector Fields
	
	[Tooltip("Sprite asset to use for positive charge.")]
	public Sprite spritePositive;

	[Tooltip("Sprite asset to use for negative charge.")]
	public Sprite spriteNegative;

	[Tooltip("Sprite asset to use for zero charge.")]
	public Sprite spriteZero;


	// Private Fields

	private SpriteRenderer sr;


	// Methods

	void Awake()
	{
		sr = GetComponent<SpriteRenderer>();


		if (!spritePositive)
		{
			Debug.LogError("Sprite Positive not assigned.", this);
		}

		if (!spriteNegative)
		{
			Debug.LogError("Sprite Negative not assigned.", this);
		}

		if (!spriteZero)
		{
			Debug.LogError("Sprite Zero not assigned.", this);
		}
	}

	public void OnChargeChanged(float charge)
	{
		if (!sr)
			return;
		
		// Switch to different sprite.
		Sprite newSprite;
		if (ChargeComparisons.IsPositive(charge))
		{
			newSprite = spritePositive;
		}
		else if (ChargeComparisons.IsNegative(charge))
		{
			newSprite = spriteNegative;
		}
		else
		{
			newSprite = spriteZero;
		}

		// Apply new sprite to renderer.
		if (newSprite)
		{
			sr.sprite = newSprite;
		}
	}
}
