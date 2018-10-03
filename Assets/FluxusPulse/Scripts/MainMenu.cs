using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void LevelMenu()
    {
        SceneManager.LoadScene(7);
    }

    public void InstructionsMenu()
    {
        SceneManager.LoadScene(2);
    }

    public void OptionsMenu()
    {
        SceneManager.LoadScene(3);
    }

    public void HighscoresMenu()
    {
        SceneManager.LoadScene(4);
    }

    public void CreditsMenu()
    {
        SceneManager.LoadScene(5);
    }

    public void QuitMenu()
    {
        SceneManager.LoadScene(6);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}