using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStateMachine : MonoBehaviour
{
    protected float skillDuration = 4f;
    protected float cooldownDuration = 5f;

    protected enum SkillState { Cooldown, Active }
    protected SkillState currentState;
    [SerializeField] protected float timer;

    protected virtual void Start()
    {
        currentState = SkillState.Cooldown;
        timer = cooldownDuration;
    }

    protected virtual void Update()
    {
        switch (currentState)
        {
            case SkillState.Cooldown:
                UpdateCooldownState();
                break;
            case SkillState.Active:
                UpdateActiveState();
                break;
        }
    }

    protected virtual void UpdateCooldownState()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            currentState = SkillState.Active;
            timer = skillDuration;
            ActivateSkill();
        }
    }

    protected virtual void UpdateActiveState()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            currentState = SkillState.Cooldown;
            timer = cooldownDuration;
        }
    }

    protected virtual void ActivateSkill()
    {
        // Perform any necessary actions when the skill becomes active
    }
}
