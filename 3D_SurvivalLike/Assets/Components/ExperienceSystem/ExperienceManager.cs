using System;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Instance;
    public delegate void ExperienceGainHandler(float expAmount);
    public delegate void ExperienceChangeHandler(int currentExp, int maxExp);
    public delegate void LevelUpHandler(int level);
    public event ExperienceGainHandler OnExperienceGain;
    public event ExperienceChangeHandler OnExperienceChange;
    public event LevelUpHandler OnLevelUp;


    private void Awake(){
        if(Instance == null){
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }else{
            Destroy(this.gameObject);
        }
        
    }

    public void GainExperience(float expAmount)
    {
        OnExperienceGain?.Invoke(expAmount);
    }
    public void UpdateExperience(int currentExp, int maxExp)
    {
        OnExperienceChange?.Invoke(currentExp, maxExp);
    }

    public void LevelUp(int level)
    {
        OnLevelUp?.Invoke(level);
        Debug.Log("Level up!");
    }

}
