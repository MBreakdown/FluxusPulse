using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    
    public GameObject[] enemiesA;
    public GameObject[] enemiesB;
    public Transform PlayerA;
    public Transform PlayerB;
    public int wave = 0;
    public int enemyCount = 0;

    private float xLocation = 0;
    private float yLocation = 0;

	// Use this for initialization
	void Start()
    {
		
	}
	
	// Update is called once per frame
	void Update()
    {
		if (wave == 0)
        {
            Spawn(1, 0, 0);
            wave++;
        }
        else if (wave == 1 && FindObjectOfType<EnemyManager>().enemyCount == 0)
        {
            Spawn(0, 1, 0);
            wave++;
        }
        else if (wave == 2 && FindObjectOfType<EnemyManager>().enemyCount == 0)
        {
            Spawn(1, 0, 1);
            wave++;
        }
        else if (wave > 2 && FindObjectOfType<EnemyManager>().enemyCount == 0)
        {
            Spawn(1, 1, 1);
            wave++;
        }
	}

    void Spawn(int bomber, int swift, int fighter)
    {
        // Bomber's spawning
        for (int i = 0; i < bomber; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                // Initialise an enemy
                GameObject go = Instantiate(enemiesA[j], new Vector2(xLocation, yLocation), Quaternion.identity);
                go.gameObject.GetComponent<EnemyScript>().playerToAvoid = PlayerA;
                go.gameObject.GetComponent<EnemyScript>().playerToFollow = PlayerB;

                // Increase the enemy count
                enemyCount += 1;
            }
            for (int j = 0; j < 2; j++)
            {
                // Initialise an enemy
                GameObject go = Instantiate(enemiesB[j], new Vector2(xLocation, yLocation), Quaternion.identity);
                go.gameObject.GetComponent<EnemyScript>().playerToAvoid = PlayerB;
                go.gameObject.GetComponent<EnemyScript>().playerToFollow = PlayerA;

                // Increase the enemy count
                enemyCount += 1;
            }

            // Send console message
            Debug.Log("Confirmed bombers spawned");
        }

        // Swift ship's spawning
        for (int i = 0; i < swift; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                // Initialise an enemy
                GameObject go = Instantiate(enemiesA[j + 2], new Vector2(xLocation, yLocation), Quaternion.identity);
                go.gameObject.GetComponent<EnemyScript>().playerToAvoid = PlayerA;
                go.gameObject.GetComponent<EnemyScript>().playerToFollow = PlayerB;

                // Increase the enemy count
                enemyCount += 1;
            }
            for (int j = 0; j < 2; j++)
            {
                // Initialise an enemy
                GameObject go = Instantiate(enemiesB[j + 2], new Vector2(xLocation, yLocation), Quaternion.identity);
                go.gameObject.GetComponent<EnemyScript>().playerToAvoid = PlayerB;
                go.gameObject.GetComponent<EnemyScript>().playerToFollow = PlayerA;

                // Increase the enemy count
                enemyCount += 1;
            }

            // Send console message
            Debug.Log("Confirmed swift ships spawned");
        }

        // Fighter's spawning
        for (int i = 0; i < fighter; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                // Initialise an enemy
                GameObject go = Instantiate(enemiesA[j + 4], new Vector2(xLocation, yLocation), Quaternion.identity);
                go.gameObject.GetComponent<EnemyScript>().playerToAvoid = PlayerA;
                go.gameObject.GetComponent<EnemyScript>().playerToFollow = PlayerB;

                // Increase the enemy count
                enemyCount += 1;
            }
            for (int j = 0; j < 2; j++)
            {
                // Initialise an enemy
                GameObject go = Instantiate(enemiesB[j + 4], new Vector2(xLocation, yLocation), Quaternion.identity);
                go.gameObject.GetComponent<EnemyScript>().playerToAvoid = PlayerB;
                go.gameObject.GetComponent<EnemyScript>().playerToFollow = PlayerA;

                // Increase the enemy count
                enemyCount += 1;
            }

            // Send console message
            Debug.Log("Confirmed swift ships spawned");
        }
    }
}