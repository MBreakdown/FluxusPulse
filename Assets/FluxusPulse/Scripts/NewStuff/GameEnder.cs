using UnityEngine;

public class GameEnder : MonoBehaviour
{
	public GameOutcome outcome = GameOutcome.Victory;

	public void EndGame()
	{
		GameController.Instance.EndGame(outcome);
	}
}
//~ class
