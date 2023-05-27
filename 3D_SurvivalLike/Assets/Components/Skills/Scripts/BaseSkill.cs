using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    protected string skillName;
    protected float damage;
    protected float cooldown;
    protected float duration;
    protected float quantity;

    protected int Level;
    protected SkillData SkillData;
    protected bool IsActive;

    void Cast(){
        Debug.Log("Cast");
    }

    protected void SetActive()
    {
        IsActive = true;
    }
    protected void UpdateStats()
    {
        //TODO  Update stats
    }

    protected void UpdateAnimation()
    {
        //TODO  Update animation
    }

}
