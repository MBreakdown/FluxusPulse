using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShipButtonType
{
	AnyPlayer,
	BothPlayers,
	AtLeastPlayerOne,
	AtLeastPlayerTwo,
	AnyPlayerButNotBoth,
	OnlyPlayerOne,
	OnlyPlayerTwo,
}
//~ enum

public class ShipButton : MonoBehaviour
{
	public ShipButtonType type = ShipButtonType.AnyPlayer;

	[SerializeField]
	private bool m_IsEventsEnabled = true;
	public bool IsEventsEnabled {
		get { return m_IsEventsEnabled; }
		set { m_IsEventsEnabled = value; }
	}

	[SerializeField]
	private BoolEventState m_Output = new BoolEventState { IsTrue = false };
	public bool Output {
		get { return m_Output.IsTrue; }
		private set
		{
			if (stayOn && Output)
				return;

			m_Output.SetIsTrue(value, fireEvents: IsEventsEnabled);
		}
	}

	[SerializeField]
	private bool stayOn = false;

	public bool IsTouchingPlayerOne { get; private set; } = false;
	public bool IsTouchingPlayerTwo { get; private set; } = false;


	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag == "Player")
		{
			PlayerShip player = collider.GetComponent<PlayerShip>();
			if (player.isPlayerOne)
				IsTouchingPlayerOne = true;
			else
				IsTouchingPlayerTwo = true;

			Check();
		}
	}

	private void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.tag == "Player")
		{
			PlayerShip player = collider.GetComponent<PlayerShip>();
			if (player.isPlayerOne)
				IsTouchingPlayerOne = false;
			else
				IsTouchingPlayerTwo = false;

			Check();
		}
	}

	private void Check()
	{
		switch (type)
		{
			case ShipButtonType.AnyPlayer:
				Output = IsTouchingPlayerOne || IsTouchingPlayerTwo;
				break;
			case ShipButtonType.BothPlayers:
				Output = IsTouchingPlayerOne && IsTouchingPlayerTwo;
				break;
			case ShipButtonType.AtLeastPlayerOne:
				Output = IsTouchingPlayerOne;
				break;
			case ShipButtonType.AtLeastPlayerTwo:
				Output = IsTouchingPlayerTwo;
				break;
			case ShipButtonType.OnlyPlayerOne:
				Output = IsTouchingPlayerOne && !IsTouchingPlayerTwo;
				break;
			case ShipButtonType.OnlyPlayerTwo:
				Output = !IsTouchingPlayerOne && IsTouchingPlayerTwo;
				break;
			case ShipButtonType.AnyPlayerButNotBoth:
				Output = IsTouchingPlayerOne ^ IsTouchingPlayerTwo;
				break;
			default:
				Debug.LogError("Unknown type.", this);
				break;
		}
	}
}
//~ class
