using UnityEngine;

public class Bush : MonoBehaviour, IDamageable
{
    private HealthSystem _healthSystem;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _health = 50;

    private void Awake()
    {
        _healthSystem = new HealthSystem(_health);

        _healthSystem.OnDead += DeadStart;
    }
    public void ApplyDamage(int amount)
    {
        _healthSystem.TakeDamage(amount);
        _animator.SetTrigger("TakeDamage");
        
    }

    private void DeadStart()
    {
        _animator.SetTrigger("Dead");
    }

    private void Dead()
    {
        Destroy(gameObject);
    }
}
