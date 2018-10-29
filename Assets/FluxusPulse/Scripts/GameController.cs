using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public float score = 0;
    public bool game = true;

    // Use this for initialization
    void Start()
    {

    }
	
	// Update is called once per frame
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
}