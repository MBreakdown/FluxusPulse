using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveDisplayText : MonoBehaviour {

	private EnemyManager em;

	public Text thisText;

	// Use this for initialization
	void Start () {
		em = FindObjectOfType<EnemyManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		thisText.text = ("WAVE  " + em.wave);
	}
}
