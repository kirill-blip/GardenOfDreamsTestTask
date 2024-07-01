using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _currentHealth = 50;
    [SerializeField] private int _maxHealth = 100;

    private HealthBar _healthBar;

    public event UnityAction EntityDied;

    private void Start()
    {
        _healthBar = GetComponentInChildren<HealthBar>();

        ResetHealth();
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    public void Heal(int hitPoint)
    {
        if (hitPoint < 0)
        {
            Debug.LogError("HitPoint can't be negative");
        }

        _currentHealth = Mathf.Min(_maxHealth, (_currentHealth + hitPoint));
        UpdateHealthBar();
    }

    public void Damage(int damage)
    {
        if (damage < 0)
        {
            Debug.LogError("Damage can't be negative");
        }

        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            EntityDied?.Invoke();
        }

        UpdateHealthBar();
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        _healthBar.SetHealth(_currentHealth);
    }
}
