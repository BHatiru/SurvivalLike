using System;
using UnityEngine;

public class GameEventSystem : MonoBehaviour
{
    public static GameEventSystem Instance;

    private void Awake()
    {
        if(Instance == null){
            Instance = this;
        }else{
            Destroy(this.gameObject);
        }
    }

    public event Action PlayerDied;
    public event Action GameLoose;
    public event Action GameWin;
    public event Action GamePaused;

    public void NotifyPlayerDied() => PlayerDied?.Invoke();
    public void NotifyGameLoose() => GameLoose?.Invoke();

    public void NotifyGameWin() => GameWin?.Invoke();

    public void NotifyGamePaused() => GamePaused?.Invoke();
}
