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
    public GameObject Player1;
    public GameObject Player2;
	public int wave = 0;
	public int enemyCount = 0;



	#endregion Public
	#region Private

	

	// Unity Event Methods
	
    void Waves()
    {
        // Spawn enemies
        Spawn(enemySpawnNum[0], enemySpawnNum[1], enemySpawnNum[2]);
        
        // Increase wave number
        wave++;

        // Heal players by 5 HP
        Player1.gameObject.GetComponent<HealthEntity>().Heal(5);
        Player2.gameObject.GetComponent<HealthEntity>().Heal(5);
    }

	void Update()
	{
		if (wave == 0)
        {
            // Set spawn numbers
            enemySpawnNum[0] = 1;
            enemySpawnNum[1] = 0;
            enemySpawnNum[2] = 0;

            // Run wave
            Waves();
		}
		else if (wave == 1 && FindObjectOfType<EnemyManager>().enemyCount == 0)
        {
            // Set spawn numbers
            enemySpawnNum[0] = 2;

            // Run wave
            Waves();
        }
		else if (wave == 2 && FindObjectOfType<EnemyManager>().enemyCount == 0)
        {
            // Set spawn numbers
            enemySpawnNum[0] = 0;
            enemySpawnNum[2] = 1;

            // Run wave
            Waves();
        }
		else if (wave == 3 && FindObjectOfType<EnemyManager>().enemyCount == 0)
        {
            // Set spawn numbers
            enemySpawnNum[0] = 0;
            enemySpawnNum[1] = 1;
            enemySpawnNum[2] = 0;

            // Run wave
            Waves();
        }
		else if (wave == 4 && FindObjectOfType<EnemyManager>().enemyCount == 0)
        {
            // Set spawn numbers
            enemySpawnNum[0] = 1;
            enemySpawnNum[1] = 0;
            enemySpawnNum[2] = 1;

            // Run wave
            Waves();
        }
		else if (wave == 5 && FindObjectOfType<EnemyManager>().enemyCount == 0)
        {
            // Set spawn numbers
            enemySpawnNum[0] = 2;

            // Run wave
            Waves();
        }
		else if (wave == 6 && FindObjectOfType<EnemyManager>().enemyCount == 0)
        {
            // Set spawn numbers
            enemySpawnNum[0] = 0;
            enemySpawnNum[1] = 1;
            enemySpawnNum[2] = 1;

            // Run wave
            Waves();
        }
		else if (wave == 7 && FindObjectOfType<EnemyManager>().enemyCount == 0)
        {
            // Set spawn numbers
            enemySpawnNum[0] = 1;
            
            // Run wave
            Waves();
        }
		else if (wave == 8 && FindObjectOfType<EnemyManager>().enemyCount == 0)
        {
            // Set spawn numbers
            enemySpawnNum[0] = 4;
            enemySpawnNum[1] = 0;
            enemySpawnNum[2] = 0;

            // Run wave
            Waves();
        }
		else if (wave == 9 && FindObjectOfType<EnemyManager>().enemyCount == 0)
        {
            // Set spawn numbers
            enemySpawnNum[0] = 0;
            enemySpawnNum[1] = 1;
            enemySpawnNum[2] = 4;

            // Run wave
            Waves();
        }
		else if (wave >= 10 && wave < 15 && FindObjectOfType<EnemyManager>().enemyCount == 0)
        {
            // Set spawn numbers
            enemySpawnNum[0] = 3;
            enemySpawnNum[2] = 2;

            // Run wave
            Waves();
        }
        else if (wave >= 15 && wave < 20 && FindObjectOfType<EnemyManager>().enemyCount == 0)
        {
            // Set spawn numbers
            enemySpawnNum[0] = 4;

            // Run wave
            Waves();
        }
        else if (wave >= 20 && wave < 25 && FindObjectOfType<EnemyManager>().enemyCount == 0)
        {
            // Set spawn numbers
            enemySpawnNum[2] = 3;

            // Run wave
            Waves();
        }
        else if (wave >= 25 && wave < 30 && FindObjectOfType<EnemyManager>().enemyCount == 0)
        {
            // Set spawn numbers
            enemySpawnNum[1] = 2;

            // Run wave
            Waves();
        }
        else if (wave >= 30 && wave < 40 && FindObjectOfType<EnemyManager>().enemyCount == 0)
        {
            // Set spawn numbers
            enemySpawnNum[1] = 3;

            // Run wave
            Waves();
        }
        else if (wave >= 40 && wave < 50 && FindObjectOfType<EnemyManager>().enemyCount == 0)
        {
            // Set spawn numbers
            enemySpawnNum[0] = 6;

            // Run wave
            Waves();
        }
        else if (wave >= 50 && wave < 60 && FindObjectOfType<EnemyManager>().enemyCount == 0)
        {
            // Set spawn numbers
            enemySpawnNum[1] = 4;
            enemySpawnNum[2] = 4;

            // Run wave
            Waves();
        }
        else if (wave >= 60 && wave < 80 && FindObjectOfType<EnemyManager>().enemyCount == 0)
        {
            // Set spawn numbers
            enemySpawnNum[0] = 8;

            // Run wave
            Waves();
        }
        else if (wave >= 80 && wave < 100 && FindObjectOfType<EnemyManager>().enemyCount == 0)
        {
            // Set spawn numbers
            enemySpawnNum[0] = 10;

            // Run wave
            Waves();
        }
        else if (wave >= 100 && FindObjectOfType<EnemyManager>().enemyCount == 0)
        {
            // Set spawn numbers
            enemySpawnNum[1] = 5;
            enemySpawnNum[2] = 5;

            // Run wave
            Waves();
        }
    }
    //~ fn

    void SpawnChange()
    {
        // Check position change
        if (xChange == true)
        {
            // Change the spawn location
            xLocation = -xLocation;

            // Change to Y change
            xChange = false;
        }
        else
        {
            // Check position change
            yLocation = -yLocation;

            // Change to X change
            xChange = true;
        }
    }

	void Spawn(int bomber, int swift, int fighter)
	{
		// Bomber's spawning
		for (int i = 0; i < bomber; i++)
		{
			// Initialise an enemy
			GameObject goA = Instantiate(enemiesA[0], new Vector2(xLocation, yLocation), Quaternion.identity);
			goA.gameObject.GetComponent<EnemyScript>().playerToAvoid = PlayerA;
			goA.gameObject.GetComponent<EnemyScript>().playerToFollow = PlayerB;

            // Change the spawn position
            SpawnChange();

            // Initialise an enemy
            GameObject goB = Instantiate(enemiesB[0], new Vector2(xLocation, yLocation), Quaternion.identity);
			goB.gameObject.GetComponent<EnemyScript>().playerToAvoid = PlayerB;
			goB.gameObject.GetComponent<EnemyScript>().playerToFollow = PlayerA;

            // Change the spawn position
            SpawnChange();

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

            // Change the spawn position
            SpawnChange();

			// Initialise an enemy
			GameObject goB = Instantiate(enemiesB[1], new Vector2(xLocation, yLocation), Quaternion.identity);
			goB.gameObject.GetComponent<EnemyScript>().playerToAvoid = PlayerB;
			goB.gameObject.GetComponent<EnemyScript>().playerToFollow = PlayerA;

            // Change the spawn position
            SpawnChange();

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
            
            // Change the spawn position
            SpawnChange();

            // Initialise an enemy
            GameObject goB = Instantiate(enemiesB[2], new Vector2(xLocation, yLocation), Quaternion.identity);
			goB.gameObject.GetComponent<EnemyScript>().playerToAvoid = PlayerB;
			goB.gameObject.GetComponent<EnemyScript>().playerToFollow = PlayerA;

            // Change the spawn position
            SpawnChange();

            // Increase the enemy count
            enemyCount += 2;

			// Send console message
			Debug.Log("Confirmed swift ships spawned");
		}
		//~ for
	}
	//~ fn



	// Private Variables

	private float xLocation = -44.5f;
	private float yLocation = 22f;
    private bool xChange = true;
    private int[] enemySpawnNum = new int[3];



	#endregion Private
}
//~ class
