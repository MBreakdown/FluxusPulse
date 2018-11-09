using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Splashcreen : MonoBehaviour {

	public bool isSplash;

	[SerializeField]
	public Image splash;

	private float timeStatic = 125.0f;
	private float alphaValue = 1.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if (isSplash) {
			if (timeStatic > 0) {
				timeStatic -= 1f;
			}


			if (timeStatic <= 0) {
				alphaValue -= 0.01f;
			}
		} else {
			alphaValue -= 0.02f;
		}

		Color temp = splash.color;
		temp.a = alphaValue;
		splash.color = temp;

		//var tempColour = splash.color;

		//tempColour.a = 0.0f;
	}
}
