/***********************************************************************
*	Bachelor of Software Engineering
*	Media Design School
*	Auckland
*	New Zealand
*
*	(c) 2018 Media Design School
*
*	File Name	:	InputScheme.cs
*	Description	:	The input axes used by a specific player.
*	Project		:	FluxusPulse
*	Team Name	:	M Breakdown Studios
*	Author		:	Elijah Shadbolt
*	Mail		:	elijah.sha7979@mediadesign.school.nz
***********************************************************************/
using UnityEngine;

public class InputScheme : MonoBehaviour
{
	#region Public



	// Public Proprties

	public float VerticalAxis => Input.GetAxis(vertical + PlayerIndex);
	public float HorizontalAxis => Input.GetAxis(horizontal + PlayerIndex);
	public bool BoostButton => Input.GetButton(boost + PlayerIndex);
	public bool FlingButton => Input.GetButton(fling + PlayerIndex);
    public bool BombButton => Input.GetButton(bomb + PlayerIndex);

    [Range(1,2)]
    public int PlayerIndex;
	// Inspector Fields

	public string vertical = "Drive";
	public string horizontal = "Horizontal";
	public string boost = "Fire1";
	public string fling = "Fire2";
    public string bomb = "Fire3";



	#endregion Public
}
//~ class
