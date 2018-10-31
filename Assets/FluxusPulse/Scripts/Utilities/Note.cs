/***********************************************************************
*	Bachelor of Software Engineering
*	Media Design School
*	Auckland
*	New Zealand
*
*	(c) 2018 Media Design School
*
*	File Name	:	Note.cs
*	Description	:	A developer's note in the scene.
*	Project		:	FluxusPulse
*	Team Name	:	M Breakdown Studios
*	Author		:	Elijah Shadbolt
*	Mail		:	elijah.sha7979@mediadesign.school.nz
***********************************************************************/
using UnityEngine;

public class Note : MonoBehaviour
{
	[TextArea(3,10)]
	public string note;


	void Awake()
	{
		Destroy(this);
	}
}
//~ class
