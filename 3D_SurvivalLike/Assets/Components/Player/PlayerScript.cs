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
}
