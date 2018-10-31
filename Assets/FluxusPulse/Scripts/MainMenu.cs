/***********************************************************************
*	Bachelor of Software Engineering
*	Media Design School
*	Auckland
*	New Zealand
*
*	(c) 2018 Media Design School
*
*	File Name	:	MainMenu.cs
*	Description	:	Component used to load different scenes.
*	Project		:	FluxusPulse
*	Team Name	:	M Breakdown Studios
***********************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	#region Public



	// Public Methods

	public void Menu()
	{
		SceneManager.LoadScene(0);
	}
	//~ fn

	public void Play()
	{
		SceneManager.LoadScene(1);
	}
	//~ fn

	public void InstructionsMenu()
	{
		SceneManager.LoadScene(2);
	}
	//~ fn

	public void OptionsMenu()
	{
		SceneManager.LoadScene(3);
	}
	//~ fn

	public void HighscoresMenu()
	{
		SceneManager.LoadScene(4);
	}
	//~ fn

	public void CreditsMenu()
	{
		SceneManager.LoadScene(5);
	}
	//~ fn

	public void QuitMenu()
	{
		SceneManager.LoadScene(6);
	}
	//~ fn

	public void LevelMenu()
	{
		SceneManager.LoadScene(7);
	}
	//~ fn

	public void CharacterMenu()
	{
		SceneManager.LoadScene(8);
	}
	//~ fn

	public void ConfirmationMenu()
	{
		SceneManager.LoadScene(9);
	}
	//~ fn

	public void Quit()
	{
		Application.Quit();
	}
	//~ fn



	#endregion Public
}
//~ class
