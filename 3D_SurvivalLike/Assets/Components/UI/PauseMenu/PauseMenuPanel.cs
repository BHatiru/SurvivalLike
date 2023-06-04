using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseMenuPanel : MonoBehaviour
{
    public Button continueButton;
    public Button restartButton;
    public Button backToStartButton;
    public Button quitButton;

    private UIManager uiManager;
    private GameManager gameManager;

    private void Start()
    {
        uiManager = UIManager.Instance;
        gameManager = GameManager.Instance;

        uiManager.HidePanel(this.gameObject);
        
        continueButton.onClick.AddListener(ContinueGame);
        restartButton.onClick.AddListener(RestartGame);
        backToStartButton.onClick.AddListener(BackToStartMenu);
        quitButton.onClick.AddListener(QuitGame);
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (uiManager.IsPanelActive(this.gameObject))
            {
                uiManager.HidePanel(this.gameObject);
                GameManager.Instance.ChangeGameState(GameManager.GameState.Gameplay);
            }
            else
            {
                
                uiManager.ShowPanel(this.gameObject);
                GameManager.Instance.ChangeGameState(GameManager.GameState.Pause);
            }
        }
    }

    private void ContinueGame()
    {
        uiManager.HidePanel(this.gameObject);
        gameManager.ResumeGame();
    }

    private void RestartGame()
    {
        gameManager.RestartGame();
    }

    private void BackToStartMenu()
    {
        gameManager.ReturnToStartMenu();
    }

    private void QuitGame()
    {
        gameManager.QuitGame();
    }
}
