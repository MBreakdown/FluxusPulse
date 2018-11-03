using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum LogicGateType
{
	And,
	Or,
	Count,
}

public class LogicGate : MonoBehaviour
{
	public LogicGateType type = LogicGateType.Or;

	[Tooltip("Only used if Type is set to Count.")]
	public int neededCountMin = 1;
	[Tooltip("Only used if Type is set to Count.")]
	public int neededCountMax = 1;

	[SerializeField]
	private bool[] inputs = new bool[2];

	[SerializeField]
	private BoolEventState m_Output = new BoolEventState { IsTrue = false };
	public bool Output {
		get { return m_Output.IsTrue; }
		private set { m_Output.IsTrue = value; }
	}

	public bool GetInput(int index)
	{
		return inputs[index];
	}

	public void SetInput(int index, bool powered)
	{
		inputs[index] = powered;
		Check();
	}

	public void SetInputTrue(int index)
	{
		SetInput(index, true);
	}

	public void SetInputFalse(int index)
	{
		SetInput(index, false);
	}

	private void Check()
	{
		switch (type)
		{
			case LogicGateType.And:
				Output = inputs.All(x => x);
				break;
			case LogicGateType.Or:
				Output = inputs.Any(x => x);
				break;
			case LogicGateType.Count:
				int count = inputs.Count(x => x);
				Output = count >= neededCountMin && count <= neededCountMax;
				break;
			default:
				Debug.LogError("Unknown type.");
				break;
		}
	}

	private void OnValidate()
	{
		if (inputs == null || inputs.Length == 0)
			inputs = new bool[1];
	}
}
//~ class
