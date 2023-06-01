using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    protected GameObject VFX_skillObj;
    protected string skillName;
    protected float damage;
    protected float cooldown;
    protected float duration;
    protected int quantity;
    protected float radius;

    protected float speed;

    [SerializeField] protected int Level;
    protected bool IsActive;

    protected float timer;

    protected Transform player;
    public SkillData _SkillData;

    protected void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    protected void SetActive()
    {
        IsActive = true;
    }
    protected void UpdateStats(SkillData data)
    {
        VFX_skillObj = data.SkillPrefab;
        SkillData.SkillLevelInfo info = data.UpgradeInfo[Level];
        skillName = info.skillName;
        damage = info.damage;
        cooldown = info.cooldown;
        duration = info.duration;
        quantity = info.quantity;
        radius = info.radius;
        speed = info.speed;
    }


    protected void UpdateModel()
    {
        //TODO  Update animation
    }

}
