using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthEntity : MonoBehaviour
{
    // Inspector Fields

    [SerializeField]
    private float m_health = 100f;

    [SerializeField]
    private float m_maxHealth = 100f;


    [System.Serializable]
    public class UnityEventFloat : UnityEvent<float> { }

    public UnityEventFloat onHealthChanged = new UnityEventFloat();
    public UnityEventFloat onHealthFractionChanged = new UnityEventFloat();

    public UnityEvent onDeath = new UnityEvent();

    public bool callDestroyOnDeath = false;
    public float destroyTime = 0f;

    
    // Properties

    public float MaxHealth { get { return m_maxHealth; } }

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

    public float HealthFraction {
        get { return Health / MaxHealth; }
        set { Health = value * MaxHealth; }
    }


    // Methods

    public void Hurt(float damage)
    {
        Health -= damage;
    }

    public void Heal(float healthIncrease)
    {
        Health += healthIncrease;
    }


    void Start()
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
