using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    // Reference to the UI panels
    public GameObject _startMenuPanel;
    public GameObject _skillSelectionPanel;
    public GameObject _pauseMenuPanel;
    public GameObject _victoryPanel;
    public GameObject _losePanel;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPanelActive(_pauseMenuPanel))
            {
                HidePanel(_pauseMenuPanel);
                GameManager.Instance.ChangeGameState(GameManager.GameState.Gameplay);
            }
            else
            {
                
                ShowPanel(_pauseMenuPanel);
                GameManager.Instance.ChangeGameState(GameManager.GameState.Pause);
            }
        }
    }


    public void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void HidePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public bool IsPanelActive(GameObject panel)
    {
        return panel.activeSelf;
    }

    public void Restart(){
        GameManager.Instance.RestartGame();
    }

    public void Quit(){
        GameManager.Instance.ReturnToStartMenu();
    }


    //TODO Add additional methods for handling UI interactions here
}

