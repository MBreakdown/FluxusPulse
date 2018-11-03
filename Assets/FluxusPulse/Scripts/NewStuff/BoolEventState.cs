/***********************************************************************
*	Bachelor of Software Engineering
*	Media Design School
*	Auckland
*	New Zealand
*
*	(c) 2018 Media Design School
*
*	File Name	:	BoolEventState.cs
*	Description	:	Boolean value with events that are fired when it is changed.
*	Project		:	FluxusPulse
*	Team Name	:	M Breakdown Studios
*	Author		:	Elijah Shadbolt
*	Mail		:	elijah.sha7979@mediadesign.school.nz
***********************************************************************/
using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class BoolEventState
{
	[SerializeField]
	private bool m_IsTrue = false;
	public bool IsTrue {
		get { return m_IsTrue; }
		set { SetIsTrue(value, fireEvents: true); }
	}

	public void SetIsTrue(bool value, bool fireEvents)
	{
		m_IsTrue = value;
		if (fireEvents)
		{
			OnChanged.Invoke(value);
			OnChangedNot.Invoke(!value);
			(value ? OnTrue : OnFalse).Invoke();
		}
	}

	public UnityEventBool OnChanged = new UnityEventBool();
	public UnityEventBool OnChangedNot = new UnityEventBool();
	public UnityEvent OnTrue = new UnityEvent();
	public UnityEvent OnFalse = new UnityEvent();


	public static explicit operator bool(BoolEventState b) { return b.m_IsTrue; }
	public static bool operator !(BoolEventState b) { return !b.m_IsTrue; }
}
//~ class
