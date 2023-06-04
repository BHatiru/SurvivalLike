using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Game states
    public enum GameState
    {
        StartMenu,
        StartGame,
        Gameplay,
        SkillSelection,
        Pause,
        Victory,
        Lose
    }

    public GameState CurrentState { get; private set; }

    private PlayerInput playerInput;
    private UIManager uiManager;

    private TimeManager timeManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        playerInput = FindObjectOfType<PlayerInput>();
    }

    private void Start()
    {

        uiManager = UIManager.Instance;
        timeManager = TimeManager.Instance;

        // Start the game in the StartMenu state
        ChangeGameState(GameState.StartMenu);
    }

    public void ChangeGameState(GameState newState)
    {
        CurrentState = newState;

        switch (CurrentState)
        {
            case GameState.StartMenu:
                // Load the StartMenu scene
                SceneManager.LoadScene("StartMenuScene");
                break;

            case GameState.StartGame:
                // Load the Gameplay scene
                StartGame();
                break;

            case GameState.Gameplay:
                //
                ResumeGame();
                break;

            case GameState.SkillSelection:
                // Show the SkillSelection panel (handled by the UIManager)
                uiManager.ShowPanel(uiManager._skillSelectionPanel);
                PauseGame();
                break;

            case GameState.Pause:
                PauseGame();
                timeManager.PauseTime();
                break;

            case GameState.Victory:
                // Display the Victory UI panel
                uiManager.ShowPanel(uiManager._victoryPanel);
                PauseGame();
                break;

            case GameState.Lose:
                // Display the Lose UI panel
                uiManager.ShowPanel(uiManager._losePanel);
                PauseGame();
                break;
        }
    }

    public void StartGame()
    {

        SceneManager.LoadScene("GameScene");
        ChangeGameState(GameState.SkillSelection);
    }

    public void PauseGame()
    {

        timeManager.PauseTime();

        //EnemyManager.Instance.PauseGeneration();

        // Disable player input
        playerInput.DeactivateInput();
    }

    public void ResumeGame()
    {

        timeManager.ResumeTime();
        //EnemyManager.Instance.PauseGeneration();
        // Enable player input
        playerInput.ActivateInput();
    }

    public void ReturnToStartMenu()
    {
        // Transition to the StartMenu state
        ChangeGameState(GameState.StartMenu);
    }

    public void RestartGame()
    {
        ChangeGameState(GameState.StartGame);
    }

    public void QuitGame()
    {
        // Quit the game (works in a build)
        Application.Quit();
    }

}
