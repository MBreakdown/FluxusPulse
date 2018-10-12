using System.Collections;
using System.Collections.Generic;
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
