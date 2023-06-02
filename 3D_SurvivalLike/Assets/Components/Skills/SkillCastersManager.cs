using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCastersManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<GameObject> skillCasters;
    void Start()
    {
        ExperienceManager.Instance.OnLevelUp += SkillChoice;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SkillChoice(int level)
    {
        switch (level)
        {
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }

}
