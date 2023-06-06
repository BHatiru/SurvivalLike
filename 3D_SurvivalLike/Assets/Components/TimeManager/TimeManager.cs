using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    public float gameTime { get; private set; } // Current game time in seconds
    [SerializeField] private float _difficultyUpInterval = 60f; // 0 seconds
    private float timeMark1 = 120f; // 2 minutes
    private float timeMark2 = 300f; // 5 minutes
    private float timeMark3 = 450; // 8 minutes
    private float timeMark4 = 600f; // 10 minutes

    private bool isPaused;
    [SerializeField] private EnemyGenerator _enemyGenerator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ResetTimer();
    }

    private void Update()
    {
        if (!isPaused)
        {
            gameTime += Time.deltaTime;
            //each minute increase the difficulty
            if (gameTime >= _difficultyUpInterval)
            {
                _difficultyUpInterval += 60f;
                _enemyGenerator.IncreaseDifficulty();
                
            }

            // Check for specific time marks
            if (gameTime >= timeMark1)
            {
                // Trigger event for time mark 1 (e.g., do something)
                //Elite enemy 1
            }

            if (gameTime >= timeMark2)
            {
                // Trigger event for time mark 2 (e.g., do something)
                //Elite enemy 2
            }
            if(gameTime >= timeMark3)
            {
                // Trigger event for time mark 3 (e.g., do something)
                // Boss
            }

            if (gameTime >= timeMark4)
            {
                // Trigger event for time mark 3 (e.g., game loss)
                GameManager.Instance.ChangeGameState(GameManager.GameState.Lose);
            }
        }
    }

    public void ResetTimer()
    {
        gameTime = 0f;
        isPaused = false;
    }

    public void PauseTime()
    {
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeTime()
    {
        isPaused = false;
        Time.timeScale = 1f;
    }
}
