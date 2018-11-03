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
	// Inspector Fields

	public float score = 0;

	public UnityEvent onVictory = new UnityEvent();
	public UnityEvent onDefeat = new UnityEvent();



	// Public Static Properties

	public static GameController Instance { get { return FindObjectOfType<GameController>(); } }



	// Public Properties

	public bool GameInProgress { get { return gameInProgress; } }
	public GameOutcome Outcome { get { return outcome; } }



	// Public Methods

	public void EndGame(GameOutcome outcome)
	{
		gameInProgress = false;
		this.outcome = outcome;

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



	// Private Fields
	
	private bool gameInProgress = true;
	private GameOutcome outcome;
}
//~ class
