/**************************************************************************
*   Bachelor of Software Engineering
*   Media Design School
*   Auckland
*   New Zealand
*
*   (c) 2018 Media Design School
*
*   File Name   :   UnclampedTimer.cs
*   Description :   Timer which can be updated with delta time.
*                   The internal time is not clamped.
*   Authors     :   Harrison Orsbourne,
*                   Vaughan Webb,
*                   Henry Oliver,
*                   Elijah Shadbolt
*   Mail        :   harrison.ors7919@mediadesign.school.nz
*                   vaughan.web8091@mediadesign.school.nz
*                   henry.oli7939@mediadesign.school.nz
*                   elijah.sha7979@mediadesign.school.nz
**************************************************************************/
using System;
using UnityEngine;

// Timer which can be updated with delta time.
// The internal time is not clamped.
//
// Example:
//   UnclampedTimerUtility timer = new UnclampedTimerUtility(5.0f); // 5 second timer
//   void Update() {
//       for (int i = timer.UpdateTimer(Time.deltaTime); i > 0; --i) {
//           Debug.Log("Timer finished.");
//       }
//   }
[Serializable]
public class UnclampedTimer
{
    // Fields and Properties

    // End time of this timer.
    [SerializeField]
    private float m_maxTime;
    public float MaxTime
    {
        get { return m_maxTime; }
        set { m_maxTime = Mathf.Max(0, value); }
    }

    // Current elapsed time of this timer.
    [SerializeField]
    private float m_currentTime;
    public float CurrentTime
    {
        get { return m_currentTime; }
        set { m_currentTime = value; }
    }

    // Returns (CurrentTime >= MaxTime).
    public bool IsFinished
    {
        get { return CurrentTime >= MaxTime; }
    }

    // Returns (MaxTime - CurrentTime).
    public float TimeLeft
    {
        get { return MaxTime - CurrentTime; }
    }

    // Returns (CurrentTime / MaxTime).
    public float TimeFraction
    {
        get
        {
            return (MaxTime == 0.0f)
                ? 0.0f
                : (CurrentTime / MaxTime);
        }
    }

    // Returns (1 - (CurrentTime / MaxTime)).
    public float TimeFractionInverse
    {
        get { return 1f - TimeFraction; }
    }


    // Constructors

    public UnclampedTimer(float maxTime, float currentTime = 0.0f)
    {
        MaxTime = maxTime;
        CurrentTime = currentTime;
    }

    public UnclampedTimer(UnclampedTimer other)
    {
        m_maxTime = other.m_maxTime;
        m_currentTime = other.m_currentTime;
    }

    public UnclampedTimer(Timer other)
    {
        MaxTime = other.MaxTime;
        CurrentTime = other.CurrentTime;
    }


    // Methods

    // Updates this timer by adding delta time to the current time.
    // Returns the number of cycles this timer finished during the update.
    public int UpdateTimer(float deltaTime)
    {
        CurrentTime += deltaTime;
        int count = 0;
        while (CurrentTime > MaxTime)
        {
            ++count;
            CurrentTime -= MaxTime;
        }
        return count;
    }

    // Resets this timer to start from 0.
    public void ResetTimer()
    {
        CurrentTime = 0.0f;
    }
}
