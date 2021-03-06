﻿/***********************************************************************
*	Bachelor of Software Engineering
*	Media Design School
*	Auckland
*	New Zealand
*
*	(c) 2018 Media Design School
*
*	File Name	:	Music.cs
*	Description	:	Persistent singleton for menu music GameObject.
*	Project		:	FluxusPulse
*	Team Name	:	M Breakdown Studios
***********************************************************************/
using UnityEngine;

public class Music : MonoBehaviour
{
	void Awake() 
	{
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
		if (objs.Length > 1)
			Destroy(this.gameObject);

		DontDestroyOnLoad(this.gameObject);
	}
	//~ fn
}
//~ class
