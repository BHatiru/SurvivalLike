using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBar;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        _healthBar.fillAmount = currentHealth / maxHealth;
    }

    void Update()
    {
        transform.LookAt(_camera.transform);
    }
}
