using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    
    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        //represent the time in the format of 00:00 (minutes:seconds)
        timerText.text = TimeManager.Instance.gameTime.ToString("00:00");
    }
}
