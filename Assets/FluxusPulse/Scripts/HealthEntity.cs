using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthEntity : MonoBehaviour
{
    // Types

    [System.Serializable]
    public class UnityEventFloat : UnityEvent<float> { }


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

    
    // Properties

    /// <summary>
    /// Maximum health of this entity.
    /// </summary>
    public float MaxHealth { get { return m_maxHealth; } }

    /// <summary>
    /// The health of this entity.
    /// </summary>
    public float Health {
        get { return m_health; }
        set
        {
            m_health = Mathf.Clamp(value, 0f, MaxHealth);
            InvokeHealthChangedEvents();

            if (m_health <= 0f)
            {
                InvokeDeathEvents();
            }
        }
    }

    /// <summary>
    /// Fraction in the range [0..1], equal to Health / MaxHealth.
    /// </summary>
    public float HealthFraction {
        get { return Health / MaxHealth; }
        set { Health = value * MaxHealth; }
    }


    // Methods

    /// <summary>
    /// Decrease Health by an amount.
    /// </summary>
    public void Hurt(float damage)
    {
        Health -= damage;
    }

    /// <summary>
    /// Increase Health by an amount.
    /// </summary>
    public void Heal(float healthIncrease)
    {
        Health += healthIncrease;
    }


    // Unity Events
    
    private void Start()
    {
        // Initialise property.
        Health = Health;
    }


    // Private Methods

    private void InvokeDeathEvents()
    {
        if (onDeath != null)
        {
            onDeath.Invoke();
        }

        if (callDestroyOnDeath)
        {
            Destroy(gameObject, destroyTime);
        }
    }

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
}