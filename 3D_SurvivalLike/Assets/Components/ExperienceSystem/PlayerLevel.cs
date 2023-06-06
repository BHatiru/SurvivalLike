using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    private int maxLevel = 21;
    [SerializeField] private int currentLevel = 0;
    [SerializeField] private float currentExperience = 0;
    private float baseExperience = 100;
    [SerializeField] private float exponent = 2f;

    public int[] expToLevelUp;
    
    private void Start()
    {
        //LevelUp();
        expToLevelUp = new int[maxLevel+1];
        Debug.Log("Calculate experience requirements");
        CalculateExperienceRequirements();
        ExperienceManager.Instance.OnExperienceGain += GainExperience;
        
        ExperienceManager.Instance.UpdateExperience(Mathf.RoundToInt(currentExperience), expToLevelUp[currentLevel]);
    }

    private void OnDestroy()
    {
        ExperienceManager.Instance.OnExperienceGain -= GainExperience;
    }

    private void CalculateExperienceRequirements(){
        for(int level = 1; level <= maxLevel; level++){
            expToLevelUp[level] = Mathf.RoundToInt(baseExperience * Mathf.Pow(exponent, level-1));
        }
    }


    public void GainExperience(float expAmount)
    {
        currentExperience += expAmount;
        
        if(currentExperience >= expToLevelUp[currentLevel] && currentLevel < maxLevel)
        {
            LevelUp();
        }
        Mathf.Clamp(currentExperience, 0, expToLevelUp[currentLevel]);
        //convert to int
        
        ExperienceManager.Instance.UpdateExperience(Mathf.RoundToInt(currentExperience), expToLevelUp[currentLevel]);
    }

    public void LevelUp()
    {
        //TODO: Add level up logic

        currentLevel++;
        currentExperience = 0;
        ExperienceManager.Instance.LevelUp(currentLevel);
    }
    
}
