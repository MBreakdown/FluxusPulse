/***********************************************************************
*	Bachelor of Software Engineering
*	Media Design School
*	Auckland
*	New Zealand
*
*	(c) 2018 Media Design School
*
*	File Name	:	GameController.cs
*	Description	:	Handles ending the level.
*	Project		:	FluxusPulse
*	Team Name	:	M Breakdown Studios
***********************************************************************/
using UnityEngine;
using UnityEngine.Events;

public enum GameOutcome
{
	Victory,
	Defeat,
}
//~ enum

public class GameController : MonoBehaviour
{
    #region Public



    // Public Static Properties

    public static GameController Instance {
        get {
            if (!m_Instance)
            {
                m_Instance = FindObjectOfType<GameController>();
                if (!m_Instance)
                    Debug.LogError("No instance of " + nameof(GameController) + " in the scene.");
            }
            return m_Instance;
        }
    }



	// Public Properties
   
	public bool GameInProgress { get; private set; }
	public GameOutcome Outcome { get; private set; }



	// Public Methods

	public void EndGame(GameOutcome outcome)
	{
		GameInProgress = false;
		this.Outcome = outcome;

		// Save the score
		PlayerPrefs.SetFloat("Highscore", score);

		switch (outcome)
		{
			case GameOutcome.Victory:
				onVictory.Invoke();
				break;
			case GameOutcome.Defeat:
				onDefeat.Invoke();
				break;
			default:
				Debug.LogError("Unknown GameOutcome.", this);
				break;
		}
	}
    //~ fn



    // Inspector Fields

    public float score = 0;

    public UnityEvent onVictory = new UnityEvent();
    public UnityEvent onDefeat = new UnityEvent();



    #endregion Public
    #region Private



    // Private Static Fields

    private static GameController m_Instance = null;



    #endregion Private
}
//~ class
