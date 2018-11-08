/***********************************************************************
*	Bachelor of Software Engineering
*	Media Design School
*	Auckland
*	New Zealand
*
*	(c) 2018 Media Design School
*
*	File Name	:	HealthEntity.cs
*	Description	:	Object with health that can be killed.
*	Project		:	FluxusPulse
*	Team Name	:	M Breakdown Studios
*	Author		:	Elijah Shadbolt
*	Mail		:	elijah.sha7979@mediadesign.school.nz
***********************************************************************/
using UnityEngine;
using UnityEngine.Events;

public class HealthEntity : MonoBehaviour
{
	#region Public
	


	// Properties

	/// <summary>
	/// Maximum health of this entity.
	/// </summary>
	public float MaxHealth { get { return m_maxHealth; } }
    public bool Invincible = false;

	/// <summary>
	/// The health of this entity.
	/// </summary>
	public float Health {
		get { return m_health; }
		set
		{
			m_health = Mathf.Clamp(value, 0f, MaxHealth);
			InvokeHealthChangedEvents();

			if (IsDead)
			{
				InvokeDeathEvents();
			}
		}
	}
	//~ prop

	/// <summary>
	/// Fraction in the range [0..1], equal to Health / MaxHealth.
	/// </summary>
	public float HealthFraction {
		get { return Health / MaxHealth; }
		set { Health = value * MaxHealth; }
	}
	//~ prop

    /// <summary>
    /// True if Health is zero.
    /// </summary>
    public bool IsDead { get { return Health <= 0; } }



	// Public Methods

	/// <summary>
	/// Decrease Health by an amount.
	/// </summary>
	public void Hurt(float damage)
	{
        // Check invisiblity
        if (!Invincible)
        {
            // Deal damage
            Health -= damage;
        }
	}

	/// <summary>
	/// Increase Health by an amount.
	/// </summary>
	public void Heal(float healthIncrease)
	{
		Health += healthIncrease;
	}

    /// <summary>
    /// Sets Health to zero and invokes death events.
    /// </summary>
    public void Kill()
    {
        Health = 0;
    }



	// Inspector Fields

	[Tooltip("Current health, in the range [0..MaxHealth].")]
	[SerializeField]
	private float m_health = 100f;

	[Tooltip("Maximum health.")]
	[SerializeField]
	private float m_maxHealth = 100f;

	[Tooltip("Event(float Health) invoked after the Health property is changed.")]
	public UnityEventFloat onHealthChanged = new UnityEventFloat();

	[Tooltip("Event(float HealthFraction) invoked after Health property is changed.")]
	public UnityEventFloat onHealthFractionChanged = new UnityEventFloat();

	[Tooltip("Event() invoked after Health property is changed.")]
	public UnityEvent onDeath = new UnityEvent();

	[Tooltip("If this is true when Health reaches 0, this GameObject will be destroyed.")]
	public bool callDestroyOnDeath = false;

	[Tooltip("Delay (seconds) before destruction after Health reaches 0. Requires CallDestroyOnDeath to be true.")]
	public float destroyTime = 0f;



	#endregion Public
	#region Private



	// Unity Events

	void Start()
	{
		// Initialise property.
		Health = Health;
	}



	// Private Methods

	private void InvokeDeathEvents()
	{
		// Check if a player has died
		if (this.GetComponent<PlayerShip>() != null)
		{
			GameController.Instance.EndGame(GameOutcome.Defeat);
		}
		// Increase the players' score
		else
		{
			GameController.Instance.Score += this.GetComponent<EnemyScript>().reward;
		}

		// Invoke the death
		if (onDeath != null)
		{
			onDeath.Invoke();
		}

		// Check to destroy the game object
		if (callDestroyOnDeath)
		{
			Destroy(gameObject, destroyTime);
		}
	}
	//~ fn

	private void InvokeHealthChangedEvents()
	{
		if (onHealthChanged != null)
		{
			onHealthChanged.Invoke(Health);
		}
		if (onHealthFractionChanged != null)
		{
			onHealthFractionChanged.Invoke(HealthFraction);
		}
	}
	//~ fn



	#endregion Private
}
//~ class
