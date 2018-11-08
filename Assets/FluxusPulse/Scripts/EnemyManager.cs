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
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

[Serializable]
public class EnemyPrefabCollection
{
    public EnemyScript bombPrefab;
    public EnemyScript fighterPrefab;
    public EnemyScript swiftPrefab;
}
//~ class

[Serializable]
public class Wave
{
    public int bombCount = 0;
    public int fighterCount = 0;
    public int swiftCount = 0;
}
//~ class

public class EnemyManager : MonoBehaviour
{
    #region Public



    // Static Properties

    public static EnemyManager Instance
    {
        get
        {
            if (!m_Instance)
            {
                m_Instance = FindObjectOfType<EnemyManager>();
                if (!m_Instance && GameController.IsGameInProgress)
                    Debug.LogError("No instance of " + nameof(EnemyManager) + " in the scene.");
            }
            return m_Instance;
        }
    }



    // Properties
    
    public int WaveIndex
    {
        get { return m_waveIndex; }
        private set { m_waveIndex = Mathf.Clamp(value, 0, waves.Length); }
    }
    //~ prop



    // Methods

    public void OnEnemySpanwed(EnemyScript enemy)
    {
        AliveEnemiesCount++;
    }

    public void OnEnemyDestroyed(EnemyScript enemy)
    {
        AliveEnemiesCount--;
    }



    // Inspector Fields

    public bool useWaves = true;
    public EnemyPrefabCollection enemies1 = new EnemyPrefabCollection();
    public EnemyPrefabCollection enemies2 = new EnemyPrefabCollection();

    [SerializeField]
    private int m_waveIndex = 0;

    public UnityEvent onNoEnemiesLeft = new UnityEvent();



    #endregion Public
    #region Private



    // Unity Event Methods

    void Awake()
    {
        // Initialise enemy counts for each wave.
        waves = new Wave[100];
        waves[0] = new Wave { bombCount = 1, fighterCount = 0, swiftCount = 0 };
        waves[1] = new Wave { bombCount = 2, fighterCount = 0, swiftCount = 0 };
        waves[2] = new Wave { bombCount = 0, fighterCount = 0, swiftCount = 1 };
        waves[3] = new Wave { bombCount = 0, fighterCount = 1, swiftCount = 0 };
        waves[4] = new Wave { bombCount = 1, fighterCount = 0, swiftCount = 1 };
        waves[5] = new Wave { bombCount = 2, fighterCount = 0, swiftCount = 1 };
        waves[6] = new Wave { bombCount = 0, fighterCount = 1, swiftCount = 1 };
        waves[7] = new Wave { bombCount = 1, fighterCount = 1, swiftCount = 1 };
        waves[8] = new Wave { bombCount = 4, fighterCount = 0, swiftCount = 0 };
        waves[9] = new Wave { bombCount = 0, fighterCount = 1, swiftCount = 4 };
        for (int i = 10; i <= 14; i++) { waves[i] = new Wave { bombCount = 3, fighterCount = 1, swiftCount = 2 }; }
        for (int i = 15; i <= 19; i++) { waves[i] = new Wave { bombCount = 4, fighterCount = 1, swiftCount = 2 }; }
        for (int i = 20; i <= 24; i++) { waves[i] = new Wave { bombCount = 1, fighterCount = 3, swiftCount = 1 }; }
        for (int i = 25; i <= 39; i++) { waves[i] = new Wave { bombCount = Random.Range(1, 5), fighterCount = Random.Range(0, 5), swiftCount = Random.Range(0, 5) }; }
        for (int i = 40; i <= 69; i++) { waves[i] = new Wave { bombCount = Random.Range(1, 10), fighterCount = Random.Range(1, 10), swiftCount = Random.Range(1, 10) }; }
        for (int i = 70; i <= 98; i++) { waves[i] = new Wave { bombCount = Random.Range(1, 20), fighterCount = Random.Range(1, 20), swiftCount = Random.Range(1, 20) }; }
        waves[99] = new Wave { bombCount = 20, fighterCount = 20, swiftCount = 20 };

        // Validate that all elements have been initialized.
        if (waves.Any(x => x == null))
        {
            Debug.LogError("Not all waves have been initialized.");
            for (int i = 0; i < waves.Length; i++)
            {
                if (waves[i] == null)
                {
                    waves[i] = new Wave { bombCount = 1 };
                }
            }
        }

        // Validate that all waves spawn at least one enemy.
        if (waves.Any(x => x.bombCount == 0 && x.fighterCount == 0 && x.swiftCount == 0))
        {
            Debug.LogError("Not all waves spawn enemies.");
        }
    }
    //~ fn

    void Start()
    {
        if (useWaves)
        {
            StartNextWave();
        }
    }
    //~ fn



    // Private Methods

    private void ChangeLocation()
    {
        // Check position change
        if (xChange == true)
        {
            // Change the spawn location
            location.x = -location.x;

            // Change to Y change
            xChange = false;
        }
        else
        {
            // Check position change
            location.y = -location.y;

            // Change to X change
            xChange = true;
        }
    }
    //~ fn

    private EnemyScript SpawnSingleEnemy(EnemyScript prefab, Vector2 position)
    {
        return Instantiate(prefab, position, Quaternion.identity);
    }
    //~ fn

    private void Spawn(Wave wave)
    {
        // Bomber's spawning
        for (int i = 0; i < wave.bombCount; i++)
        {
            // Initialise an enemy
            SpawnSingleEnemy(enemies1.bombPrefab, location);
            SpawnSingleEnemy(enemies2.bombPrefab, location);

            // Change the spawn position
            ChangeLocation();
        }
        //~ for

        // Swift ship's spawning
        for (int i = 0; i < wave.swiftCount; i++)
        {
            // Initialise an enemy
            SpawnSingleEnemy(enemies1.swiftPrefab, location);
            SpawnSingleEnemy(enemies2.swiftPrefab, location);

            // Change the spawn position
            ChangeLocation();
        }
        //~ for

        // Fighter's spawning
        for (int i = 0; i < wave.fighterCount; i++)
        {
            // Initialise an enemy
            SpawnSingleEnemy(enemies1.fighterPrefab, location);
            SpawnSingleEnemy(enemies2.fighterPrefab, location);

            // Change the spawn position
            ChangeLocation();
        }
        //~ for
    }
    //~ fn

    private void SpawnNextWaveNow()
    {
        // Heal players by 5 HP
        PlayerShip ship1 = PlayerShip.GetPlayer1;
        if (ship1 && ship1.healthEntity)
            ship1.healthEntity.Heal(5);

        PlayerShip ship2 = PlayerShip.GetPlayer2;
        if (ship2 && ship2.healthEntity)
            ship2.healthEntity.Heal(5);

        // Spawn enemies
        Spawn(waves[WaveIndex]);

        // Increase wave number
        WaveIndex++;
    }
    //~ fn

    // Finnicky function to wait one frame before spawning.
    // This is stopped if this GameObject is destroyed between frames.
    private IEnumerator StartNextWaveWait()
    {
        yield return null;
        SpawnNextWaveNow();
    }
    //~ fn

    private void StartNextWave()
    {
        StartCoroutine(StartNextWaveWait());
    }
    //~ fn



    // Private Properties

    private int AliveEnemiesCount
    {
        get { return m_aliveEnemiesCount; }
        set
        {
            // Clamp value to at least 0
            m_aliveEnemiesCount = value < 0 ? 0 : value;

            // If no more enemies alive, invoke the event.
            if (m_aliveEnemiesCount <= 0)
            {
                if (useWaves)
                {
                    StartNextWave();
                }

                if (onNoEnemiesLeft != null)
                {
                    onNoEnemiesLeft.Invoke();
                }
            }
        }
    }
    //~ prop



    // Private Static Fields

    private static EnemyManager m_Instance = null;



    // Private Fields
    
    private Wave[] waves;
    private int m_aliveEnemiesCount = 0;

    private Vector2 location = new Vector2(-44.5f, 22f);
    private bool xChange = true;
    


    #endregion Private
}
//~ class
