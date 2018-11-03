using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotGate : MonoBehaviour
{
	[SerializeField]
	private BoolEventState m_Output = new BoolEventState { IsTrue = false };
	public bool Output {
		get { return m_Output.IsTrue; }
		set { m_Output.IsTrue = value; }
	}

	public bool Input {
		get { return !Output; }
		set { Output = !value; }
	}
}
//~ class
