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
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	// Inspector Fields

	public float score = 0;
	public bool game = true;
	


	// Unity Event Methods

	void Update()
	{
		// Check if the game is over
		if (game == false)
		{
			// Save the score
			PlayerPrefs.SetFloat("Highscore", score);
			
			// Go to the menu
			SceneManager.LoadScene(10);
		}
	}
	//~ fn
}
//~ class
