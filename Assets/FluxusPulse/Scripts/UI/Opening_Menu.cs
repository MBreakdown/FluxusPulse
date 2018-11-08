using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Opening_Menu : MonoBehaviour {

	[SerializeField]
	private string nextScene;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			SceneManager.LoadScene (nextScene);
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
			Debug.Log ("I have quit");
		}
	}
}
