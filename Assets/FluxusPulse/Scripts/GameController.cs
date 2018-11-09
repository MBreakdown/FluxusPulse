/***********************************************************************
*	Bachelor of Software Engineering
*	Media Design School
*	Auckland
*	New Zealand
*
*	(c) 2018 Media Design School
*
*	File Name	:	GameController.cs
*	Description	:	Handles ending the level.
*	Project		:	FluxusPulse
*	Team Name	:	M Breakdown Studios
***********************************************************************/
using UnityEngine;
using UnityEngine.Events;

public enum GameOutcome
{
	Victory,
	Defeat,
}
//~ enum

public class GameController : MonoBehaviour
{
    #region Public

    public AudioSource explosion;
    public AudioSource pos;
    public AudioSource hurt;
    public AudioSource shoot;

    // Public Static Properties

    public static GameController Instance {
        get {
            if (!m_Instance)
            {
                m_Instance = FindObjectOfType<GameController>();
                if (!m_Instance && IsGameInProgress)
                    Debug.LogError("No instance of " + nameof(GameController) + " in the scene.");
            }
            return m_Instance;
        }
    }

    public static bool IsGameInProgress { get; private set; } = true;



    // Public Properties

    public GameOutcome Outcome { get; private set; }

    public bool IsPaused {
        get { return m_isPaused; }
        set
        {
            m_isPaused = value;
            PauseMenu.SetActive(m_isPaused);
            Time.timeScale = m_isPaused ? 0 : 1;
        }
    }
    //~ prop

    public float Score
    {
        get { return m_score; }
        set
        {
            m_score = value;
            if (onScoreChanged != null) { onScoreChanged.Invoke(m_score); }
        }
    }
    //~ prop



	// Public Methods

	public void EndGame(GameOutcome outcome)
	{
        if (!IsGameInProgress)
            return;

		IsGameInProgress = false;
		this.Outcome = outcome;

		// Save the score
		PlayerPrefs.SetFloat("Highscore", m_score);

		switch (outcome)
		{
			case GameOutcome.Victory:
				onVictory.Invoke();
				break;
			case GameOutcome.Defeat:
				onDefeat.Invoke();
				break;
			default:
				Debug.LogError("Unknown GameOutcome.", this);
				break;
		}
	}
    //~ fn



    // Inspector Fields
    
    public GameObject PauseMenu;

    public UnityEventFloat onScoreChanged = new UnityEventFloat();
    public UnityEvent onVictory = new UnityEvent();
    public UnityEvent onDefeat = new UnityEvent();



    #endregion Public
    #region Private



    // Unity Event Methods

    void Awake()
    {
        IsGameInProgress = true;
    }
    //~ fn

    void Start()
    {
        IsPaused = IsPaused;
    }
    //~ fn

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            IsPaused = !IsPaused;
        }
    }
    //~ fn

    void OnDestroy()
    {
        // If this was the only instance and it was destroyed,
        if (FindObjectsOfType<GameController>().Length == 0)
        {
            IsGameInProgress = false;
        }
    }
    //~ fn



    // Private Static Fields

    private static GameController m_Instance = null;



    // Private Fields

    private float m_score = 0;

    private bool m_isPaused = false;



    #endregion Private
}
//~ class
