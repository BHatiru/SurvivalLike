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
    protected bool IsActive;

    protected float timer;


    protected void SetActive()
    {
        IsActive = true;
    }
    protected void UpdateStats(SkillData data)
    {
        SkillData.SkillLevelInfo info = data.UpgradeInfo[Level];
        skillName = info.skillName;
        damage = info.damage;
        cooldown = info.cooldown;
        duration = info.duration;
        quantity = info.quantity;
    }

    protected void UpdateAnimation()
    {
        //TODO  Update animation
    }

}
