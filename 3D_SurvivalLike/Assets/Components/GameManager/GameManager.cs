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

    [SerializeField] public GameState CurrentState { get; private set; }

    [SerializeField]private PlayerInput playerInput;
    private UIManager uiManager;

    private TimeManager timeManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

        uiManager = UIManager.Instance;
        timeManager = TimeManager.Instance;
        ExperienceManager.Instance.OnLevelUp += OnLevelUp;
        ChangeGameState(GameState.StartGame);
        // Start the game in the StartMenu state
        //ChangeGameState(GameState.StartMenu);
    }
    


    void Update()
    {
        if(playerInput == null)
            playerInput = FindObjectOfType<PlayerInput>();
    }

    private void OnLevelUp(int level)
    {
        ChangeGameState(GameState.SkillSelection);
    }

    public void ChangeGameState(GameState newState)
    {
        CurrentState = newState;

        switch (CurrentState)
        {
            case GameState.StartMenu:
                Debug.Log("StartMenu");
                SceneManager.LoadScene("StartMenuScene");
                // uiManager.ShowPanel(uiManager._startMenuPanel);
                // PauseGame();
                break;

            case GameState.StartGame:
                
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
        ResumeGame();
        //ChangeGameState(GameState.SkillSelection);
    }

    public void CloseAllPanels()
    {
        uiManager.HidePanel(uiManager._startMenuPanel);
        uiManager.HidePanel(uiManager._skillSelectionPanel);
        uiManager.HidePanel(uiManager._pauseMenuPanel);
        uiManager.HidePanel(uiManager._victoryPanel);
        uiManager.HidePanel(uiManager._losePanel);
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
        //EnemyManager.Instance.ResumeGeneration();
        // Enable player input
        playerInput.ActivateInput();

        //close all panels
        CloseAllPanels();
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

    void OnDestroy()
    {
        ExperienceManager.Instance.OnLevelUp -= OnLevelUp;
    }

}
