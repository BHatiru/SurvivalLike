using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;
using System;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    
    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(TimeManager.Instance.gameTime);
        string formattedTime = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
        //represent the time in the format of mm:ss
        timerText.text = formattedTime;
    }
}
