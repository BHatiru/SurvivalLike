using UnityEngine;
using UnityEngine.InputSystem;

public class GamePauseManager : MonoBehaviour
{
    public static GamePauseManager Instance { get; private set; }

    private bool isGamePaused = false;
    private PlayerInput playerInput;

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

        playerInput = FindObjectOfType<PlayerInput>();
    }

    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f;

        // Disable player input
        playerInput.DeactivateInput();
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;

        // Enable player input
        playerInput.ActivateInput();
    }

    public bool IsGamePaused()
    {
        return isGamePaused;
    }

    // Add any other methods or functionality related to game pause management
}
