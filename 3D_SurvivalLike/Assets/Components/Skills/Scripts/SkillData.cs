using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills/SkillData")]
public class SkillData : ScriptableObject
{

    public Sprite skillIcon;
    public GameObject SkillVFX;
    public List<SkillLevelInfo> UpgradeInfo;
    internal string skillName;

    [System.Serializable]
    public class SkillLevelInfo
    {
        public string skillName;
        public float damage;
        public float cooldown;
        public float duration;
        public float quantity;
    }
}

