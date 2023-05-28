using System;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Instance;
    public delegate void ExperienceGainHandler(int expAmount);
    public delegate void ExperienceChangeHandler(int currentExp, int maxExp);
    public delegate void LevelUpHandler(int level);
    public event ExperienceGainHandler OnExperienceGain;
    public event ExperienceChangeHandler OnExperienceChange;
    public event LevelUpHandler OnLevelUp;


    private void Awake(){
        if(Instance == null){
            Instance = this;
        }else{
            Destroy(this.gameObject);
        }
    }

    public void GainExperience(int expAmount)
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
    }

}
