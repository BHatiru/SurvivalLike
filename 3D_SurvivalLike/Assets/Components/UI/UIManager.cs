using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    // Reference to the UI panels
    public GameObject _skillSelectionPanel;
    public GameObject _pauseMenuPanel;
    public GameObject _victoryPanel;
    public GameObject _losePanel;

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



    //TODO Add additional methods for handling UI interactions here
}

