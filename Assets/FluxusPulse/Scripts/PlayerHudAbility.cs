/***********************************************************************
*	Bachelor of Software Engineering
*	Media Design School
*	Auckland
*	New Zealand
*
*	(c) 2018 Media Design School
*
*	File Name	:	PlayerHudAbility.cs
*	Description	:	Heads-up display for a player.
*	Project		:	FluxusPulse
*	Team Name	:	M Breakdown Studios
***********************************************************************/
using UnityEngine;
using UnityEngine.UI;

public class PlayerHudAbility : MonoBehaviour
{
    // Public Properties

    public PlayerShip Player { get; private set; }



    // Inspector Fields

    public Text keyText;
    public Image fillImage;
    public string keyboardKeyName = "J";
    public string controllerButtonName = "X";



    // Unity Event Methods

    void Awake()
    {
        Player = GetComponentInParent<PlayerShip>();
        if (!Player)
            Debug.LogError("Player not found.");
    }

    void Start()
    {
        keyText.text = keyboardKeyName;
    }
}
//~ class
