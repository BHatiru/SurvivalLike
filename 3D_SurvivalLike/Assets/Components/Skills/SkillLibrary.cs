using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill Library", menuName = "Skills/Skills Library")]
public class SkillLibrary : ScriptableObject
{
    public List<SkillData> Library;
}