using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private Transform _dependantSkillContainer;
    [SerializeField] private Transform _independentSkillContainer;

    [SerializeField] private SkillLibrary _skillLibrary;

    public void ObtainSkill(SkillData skillData)
    {
        GameObject skill = Instantiate(skillData.SkillPrefab);
    }
}
