using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _currentHealth = 50;
    [SerializeField] private int _maxHealth = 100;

    private HealthBar _healthBar;

    private void Start()
    {
        _healthBar = GetComponentInChildren<HealthBar>();
        _healthBar.SetHealth(_currentHealth);
    }

    private void Update()
    {
        _healthBar.SetHealth(_currentHealth);
    }

    public void Heal(int hitPoint)
    {
        _currentHealth = Mathf.Min(_maxHealth, (_currentHealth + hitPoint));
        _healthBar.SetHealth(_currentHealth);
    }
}
