using R3;
using System;
using UnityEngine;

public class HealthSystem
{
    private ReactiveProperty<int> _maxHealth = new();
    private ReactiveProperty<int> _currentHealth = new();

    public ReadOnlyReactiveProperty<int> MaxHealth => _maxHealth;
    public ReadOnlyReactiveProperty<int> CurrentHealth => _currentHealth;

    public event Action OnDead;

    public HealthSystem(int maxHp)
    {
        _maxHealth.Value = maxHp;
        _currentHealth.Value = maxHp;
    }

    public void TakeDamage(int amount)
    {
        _currentHealth.Value = Mathf.Max(0, _currentHealth.Value - amount);

        if (_currentHealth.Value == 0) Dead();
    }

    public void Dead()
    {
        //Умер
        OnDead?.Invoke();
    }
}
