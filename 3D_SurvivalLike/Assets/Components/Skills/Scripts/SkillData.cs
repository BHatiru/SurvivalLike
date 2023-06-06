using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills/SkillData")]
public class SkillData : ScriptableObject
{

    public Sprite skillIcon;
    public GameObject SkillPrefab;
    public GameObject SkillCaster;

    public string SkillName;

    public enum SkillType
    {
        Fire,
        Ice,
        Lightning
    };

    public SkillType skillType;

    public Dictionary<SkillType, Color> SkillColor = new ()
    {
        {SkillType.Fire, Color.red},
        {SkillType.Ice, Color.blue},
        {SkillType.Lightning, Color.yellow}
    };
    
    public int CurrentLevel = -1;
    public int SkillMaxLvl = 3;

    public List<SkillLevelInfo> UpgradeInfo;

    [System.Serializable]
    public class SkillLevelInfo
    {
        public string skillName;
        public float damage;
        public float cooldown;
        public float duration;
        public int quantity;
        public float radius;
        public float speed;
    }
}

