/***********************************************************************
*	Bachelor of Software Engineering
*	Media Design School
*	Auckland
*	New Zealand
*
*	(c) 2018 Media Design School
*
*	File Name	:	Vector2Utilities.cs
*	Description	:	Utility functions for Vector2.
*	Project		:	FluxusPulse
*	Team Name	:	M Breakdown Studios
*	Author		:	Elijah Shadbolt
*	Mail		:	elijah.sha7979@mediadesign.school.nz
***********************************************************************/
using UnityEngine;

public static class Vector2Utilities
{
	#region Public



	public static Vector2 Rotate(Vector2 v, float radians)
	{
		float sin = Mathf.Sin(radians);
		float cos = Mathf.Cos(radians);
		return new Vector2(cos * v.x - sin * v.y, sin * v.x + cos * v.y);
	}
	//~ fn

	public static float CalculateArcAngle(float radius, float arcLength)
	{
		return arcLength / radius;
	}
	//~ fn

	public static Vector2 Perpendicular(Vector2 vector)
	{
		return new Vector2(-vector.y, vector.x);
	}
	//~ fn



	#endregion Public
}
//~ class
