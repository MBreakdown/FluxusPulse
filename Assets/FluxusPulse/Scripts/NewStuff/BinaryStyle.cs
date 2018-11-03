using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SprStyle
{
	public Sprite sprite;
	public Color color = Color.white;

	public void Apply(SpriteRenderer renderer)
	{
		renderer.sprite = sprite;
		renderer.color = color;
	}
}
//~ class

[ExecuteInEditMode]
public class BinaryStyle : MonoBehaviour
{
	[SerializeField]
	private bool m_Powered = false;
	public bool Powered { get { return m_Powered; }
		set
		{
			m_Powered = value;
			SprStyle style = m_Powered ? stylePowered : styleUnpowered;
			Array.ForEach(renderers, style.Apply);
		}
	}

	public SpriteRenderer[] renderers;
	public SprStyle styleUnpowered = new SprStyle();
	public SprStyle stylePowered = new SprStyle();

	
	private void Update()
	{
		Powered = Powered;
	}
}
//~ class
