using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills/SkillData")]
public class SkillData : ScriptableObject
{

    public Sprite skillIcon;
    public GameObject SkillPrefab;
    public List<SkillLevelInfo> UpgradeInfo;

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

