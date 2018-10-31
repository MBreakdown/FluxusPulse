/***********************************************************************
*	Bachelor of Software Engineering
*	Media Design School
*	Auckland
*	New Zealand
*
*	(c) 2018 Media Design School
*
*	File Name	:	UnityEventSpecializations.cs
*	Description	:	Specializations of the generic UnityEvent<T> class.
*	Project		:	FluxusPulse
*	Team Name	:	M Breakdown Studios
*	Author		:	Elijah Shadbolt
*	Mail		:	elijah.sha7979@mediadesign.school.nz
***********************************************************************/
using System;
using UnityEngine.Events;

[Serializable]
public class UnityEventFloat : UnityEvent<float> { }

[Serializable]
public class UnityEventInt : UnityEvent<int> { }

[Serializable]
public class UnityEventBool : UnityEvent<bool> { }
