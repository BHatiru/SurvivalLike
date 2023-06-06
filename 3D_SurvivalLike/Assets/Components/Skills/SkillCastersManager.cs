using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCastersManager : MonoBehaviour
{
    public static SkillCastersManager Instance { get; private set; }

    [SerializeField] private SkillLibrary skillLibrary;
    // [SerializeField] private GameObject _skillCastersMaster;
    public List<SkillData> acquiredSkills; //what skills the player has already acquired
    public List<SkillData> nonacquiredSkills; //what skill types the player has not yet acquired
    private Dictionary<SkillData, SkillUpgrader> _skillCasters;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        acquiredSkills = new List<SkillData>();
        nonacquiredSkills = new List<SkillData>();
        _skillCasters = new Dictionary<SkillData, SkillUpgrader>();
        foreach (SkillData skill in skillLibrary.Library)
        {
            nonacquiredSkills.Add(skill);
            skill.CurrentLevel = -1;
        }
    }

    void Update()
    {
        // if(_skillCastersMaster == null)
        // {
        //     _skillCastersMaster = FindObjectOfType<SkillHolder>().gameObject;
        // }
    }

    public void AcquireNewSkill(SkillData skill)
    {
        Debug.Log("Acquire new skill");
        acquiredSkills.Add(skill);
        nonacquiredSkills.Remove(skill);
        ActivateSkillCaster(skill);
        skill.CurrentLevel = 1;
    }

    public void AcquireUpgrade(SkillData skill)
    {
        Debug.Log("Acquire upgrade");
        //skill.SkillCaster?.GetComponent<ICaster>().UpdateCasterLevel();
        _skillCasters[skill].UpgradeSkill();
    }

    public void ActivateSkillCaster(SkillData skill){
        Debug.Log("Activate skill caster");
        //skill.SkillCaster?.GetComponent<BaseSkill>().ActivateSkillCaster();
        var sklObj = Instantiate(skill.SkillCaster, transform);

        _skillCasters.Add(skill, sklObj.GetComponent<SkillUpgrader>());
    }


}
