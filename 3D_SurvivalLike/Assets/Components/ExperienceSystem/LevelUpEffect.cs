using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpEffect : MonoBehaviour
{
    private ParticleSystem lvlUpEffect;
    private void Start()
    {
        lvlUpEffect = GetComponent<ParticleSystem>();
        ExperienceManager.Instance.OnLevelUp += PlayEffect;
    }

    private void PlayEffect(int level)
    {
        if(level > 1)
        lvlUpEffect.Play();
    }

    private void OnDestroy()
    {
        ExperienceManager.Instance.OnLevelUp -= PlayEffect;
    }
}
