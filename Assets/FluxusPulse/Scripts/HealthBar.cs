/***********************************************************************
*	Bachelor of Software Engineering
*	Media Design School
*	Auckland
*	New Zealand
*
*	(c) 2018 Media Design School
*
*	File Name	:	HealthEntity.cs
*	Description	:	Object with health that can be killed.
*	Project		:	FluxusPulse
*	Team Name	:	M Breakdown Studios
*	Author		:	Elijah Shadbolt
*	Mail		:	elijah.sha7979@mediadesign.school.nz
***********************************************************************/
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	#region Public
	


	// Properties
    
	public float FillFraction {
		get { return m_image.fillAmount; }
		set { m_image.fillAmount = value; }
	}
	//~ prop



	// Inspector Fields
    
	[SerializeField]
	private Image m_image;

    [SerializeField]
    [Range(1, 2)]
    private int m_playerIndex = 1;



	#endregion Public
	#region Private



	// Unity Events

	void Start()
	{
        // Add event listener so this health bar will update automatically.
        PlayerShip.GetPlayer(m_playerIndex)
            .GetComponent<HealthEntity>()
            .onHealthFractionChanged
            .AddListener((fraction) => FillFraction = fraction);
	}



	#endregion Private
}
//~ class
