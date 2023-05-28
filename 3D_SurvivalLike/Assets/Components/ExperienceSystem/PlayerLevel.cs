using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    private int maxLevel = 10;
    private int currentLevel = 1;
    [SerializeField] private int currentExperience = 0;
    private int baseExperience = 100;
    [SerializeField] private float exponent = 2f;
    [SerializeField] private float _attractionRadius = 7f;
    public int[] expToLevelUp;

    private void Start()
    {
        expToLevelUp = new int[maxLevel+1];
        CalculateExperienceRequirements();
        ExperienceManager.Instance.OnExperienceGain += GainExperience;
        ExperienceManager.Instance.LevelUp(currentLevel);
        ExperienceManager.Instance.UpdateExperience(currentExperience, expToLevelUp[currentLevel]);
    }

    private void OnDisable()
    {
        ExperienceManager.Instance.OnExperienceGain -= GainExperience;
    }

    private void CalculateExperienceRequirements(){
        for(int level = 1; level <= maxLevel; level++){
            expToLevelUp[level] = Mathf.RoundToInt(baseExperience * Mathf.Pow(exponent, level-1));
        }
    }



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _attractionRadius);
    }

    public void GainExperience(int expAmount)
    {
        currentExperience += expAmount;
        
        ExperienceManager.Instance.UpdateExperience(currentExperience, expToLevelUp[currentLevel]);

        if(currentExperience >= expToLevelUp[currentLevel] && currentLevel < maxLevel)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {

        
        //TODO: Add level up logic

        currentLevel++;
        currentExperience = 0;
        ExperienceManager.Instance.LevelUp(currentLevel);
        ExperienceManager.Instance.UpdateExperience(currentExperience, expToLevelUp[currentLevel]);
    }
    
}
