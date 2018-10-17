using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public GameObject bomberNeg1;
    public GameObject bomberNeg2;
    public GameObject bomberPos1;
    public GameObject bomberPos2;
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
            Spawn(1);
            wave++;
        }
        // Get the enemy count
        /*else if (wave == 1)
        {
            Spawn(2);
            wave++;
        }*/
	}

    void Spawn(int bomber)
    {
        for (int i = 0; i < bomber; i++)
        {
            Instantiate(bomberNeg1, new Vector2(xLocation, yLocation), Quaternion.identity);
            Instantiate(bomberNeg2, new Vector2(xLocation, yLocation), Quaternion.identity);
            Instantiate(bomberPos1, new Vector2(xLocation, yLocation), Quaternion.identity);
            Instantiate(bomberPos2, new Vector2(xLocation, yLocation), Quaternion.identity);
            xLocation += 0.5f;
            yLocation += 0.5f;

            Debug.Log("Confirmed bomber spawn");
        }
    }
}