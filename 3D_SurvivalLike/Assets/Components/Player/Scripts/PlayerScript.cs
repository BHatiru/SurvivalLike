using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _currentHealth;

    [SerializeField] private HealthBar _healthBar;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _healthBar.UpdateHealthBar(_currentHealth, _maxHealth);
    }
    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        _healthBar.UpdateHealthBar(_currentHealth, _maxHealth);
        Debug.Log("Player took " + damage + " damage");
        if (_currentHealth <= 0f)
        {
            Die();
        }
    }

    public void Die(){
        GameEventSystem.Instance.NotifyPlayerDied();
        Debug.Log("Player died");
    }

    public void Heal (float healAmount)
    {
        if(_currentHealth <= 0f) return;
        
        _currentHealth += healAmount;
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        _healthBar.UpdateHealthBar(_currentHealth, _maxHealth);
        Debug.Log("Player healed " + healAmount + " health");
    }

    //Will be accessed from different script, for now test holder
    private void LearnSkill(SkillLibrary skillLibrary, int skillIndex)
    {
        //TODO
        SkillData skill = skillLibrary.Library[skillIndex];
    }
}
