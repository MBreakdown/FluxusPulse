/***********************************************************************
*	Bachelor of Software Engineering
*	Media Design School
*	Auckland
*	New Zealand
*
*	(c) 2018 Media Design School
*
*	File Name	:	EnemyManager.cs
*	Description	:	Manages the spawning and wave system of enemies.
*	Project		:	FluxusPulse
*	Team Name	:	M Breakdown Studios
***********************************************************************/
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	#region Public



	// Inspector Fields

	public GameObject[] enemiesA;
	public GameObject[] enemiesB;
	public Transform PlayerA;
	public Transform PlayerB;
	public int wave = 0;
	public int enemyCount = 0;



	#endregion Public
	#region Private

	

	// Unity Event Methods
	
	void Update()
	{
		if (wave == 0)
		{
			Spawn(1, 0, 0);
			wave++;
		}
		else if (wave == 1 && FindObjectOfType<EnemyManager>().enemyCount == 0)
		{
			Spawn(2, 0, 0);
			wave++;
		}
		else if (wave == 2 && FindObjectOfType<EnemyManager>().enemyCount == 0)
		{
			Spawn(0, 1, 0);
			wave++;
		}
		else if (wave == 3 && FindObjectOfType<EnemyManager>().enemyCount == 0)
		{
			Spawn(0, 0, 1);
			wave++;
		}
		else if (wave == 4 && FindObjectOfType<EnemyManager>().enemyCount == 0)
		{
			Spawn(1, 0, 1);
			wave++;
		}
		else if (wave == 5 && FindObjectOfType<EnemyManager>().enemyCount == 0)
		{
			Spawn(2, 0, 1);
			wave++;
		}
		else if (wave == 6 && FindObjectOfType<EnemyManager>().enemyCount == 0)
		{
			Spawn(0, 1, 1);
			wave++;
		}
		else if (wave == 7 && FindObjectOfType<EnemyManager>().enemyCount == 0)
		{
			Spawn(1, 1, 1);
			wave++;
		}
		else if (wave == 8 && FindObjectOfType<EnemyManager>().enemyCount == 0)
		{
			Spawn(4, 0, 0);
			wave++;
		}
		else if (wave == 9 && FindObjectOfType<EnemyManager>().enemyCount == 0)
		{
			Spawn(0, 1, 4);
			wave++;
		}
		else if (wave >= 10 && FindObjectOfType<EnemyManager>().enemyCount == 0)
		{
			Spawn(2, 1, 2);
			wave++;
		}
	}
	//~ fn

	void Spawn(int bomber, int swift, int fighter)
	{
		// Bomber's spawning
		for (int i = 0; i < bomber; i++)
		{
			// Initialise an enemy
			GameObject goA = Instantiate(enemiesA[0], new Vector2(xLocation, yLocation), Quaternion.identity);
			goA.gameObject.GetComponent<EnemyScript>().playerToAvoid = PlayerA;
			goA.gameObject.GetComponent<EnemyScript>().playerToFollow = PlayerB;

			// Initialise an enemy
			GameObject goB = Instantiate(enemiesB[0], new Vector2(xLocation, yLocation), Quaternion.identity);
			goB.gameObject.GetComponent<EnemyScript>().playerToAvoid = PlayerB;
			goB.gameObject.GetComponent<EnemyScript>().playerToFollow = PlayerA;

			// Increase the enemy count
			enemyCount += 2;

			// Send console message
			Debug.Log("Confirmed bombers spawned");
		}
		//~ for

		// Swift ship's spawning
		for (int i = 0; i < swift; i++)
		{
			// Initialise an enemy
			GameObject goA = Instantiate(enemiesA[1], new Vector2(xLocation, yLocation), Quaternion.identity);
			goA.gameObject.GetComponent<EnemyScript>().playerToAvoid = PlayerA;
			goA.gameObject.GetComponent<EnemyScript>().playerToFollow = PlayerB;

			// Initialise an enemy
			GameObject goB = Instantiate(enemiesB[1], new Vector2(xLocation, yLocation), Quaternion.identity);
			goB.gameObject.GetComponent<EnemyScript>().playerToAvoid = PlayerB;
			goB.gameObject.GetComponent<EnemyScript>().playerToFollow = PlayerA;

			// Increase the enemy count
			enemyCount += 2;

			// Send console message
			Debug.Log("Confirmed swift ships spawned");
		}
		//~ for

		// Fighter's spawning
		for (int i = 0; i < fighter; i++)
		{
			// Initialise an enemy
			GameObject goA = Instantiate(enemiesA[2], new Vector2(xLocation, yLocation), Quaternion.identity);
			goA.gameObject.GetComponent<EnemyScript>().playerToAvoid = PlayerA;
			goA.gameObject.GetComponent<EnemyScript>().playerToFollow = PlayerB;

			// Initialise an enemy
			GameObject goB = Instantiate(enemiesB[2], new Vector2(xLocation, yLocation), Quaternion.identity);
			goB.gameObject.GetComponent<EnemyScript>().playerToAvoid = PlayerB;
			goB.gameObject.GetComponent<EnemyScript>().playerToFollow = PlayerA;

			// Increase the enemy count
			enemyCount += 2;

			// Send console message
			Debug.Log("Confirmed swift ships spawned");
		}
		//~ for
	}
	//~ fn



	// Private Constants

	private const float xLocation = 25;
	private const float yLocation = -12;



	#endregion Private
}
//~ class
