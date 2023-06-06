using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUpgrader : MonoBehaviour
{
    private ICaster caster;
    void Start()
    {
        caster = GetComponent<ICaster>();
        
    }

    public void UpgradeSkill()
    {
        caster.UpdateCasterLevel();
    }
}
