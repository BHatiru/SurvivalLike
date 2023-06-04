using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    public float gameTime { get; private set; } // Current game time in seconds
    private float timeMark1 = 120f; // 2 minutes
    private float timeMark2 = 300f; // 5 minutes
    private float timeMark3 = 600f; // 10 minutes

    private bool isPaused;

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

            // Check for specific time marks
            if (gameTime >= timeMark1)
            {
                // Trigger event for time mark 1 (e.g., do something)
            }

            if (gameTime >= timeMark2)
            {
                // Trigger event for time mark 2 (e.g., do something)
            }

            if (gameTime >= timeMark3)
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
