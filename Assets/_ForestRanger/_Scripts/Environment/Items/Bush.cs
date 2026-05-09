using UnityEngine;

public class Bush : MonoBehaviour, IDamageable
{
    private HealthSystem _healthSystem;
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _healthSystem = new HealthSystem(50);

        _healthSystem.OnDead += Dead;
    }
    public void ApplyDamage(int amount)
    {
        _healthSystem.TakeDamage(amount);
        _animator.SetTrigger("TakeDamage");
        
    }

    private void Dead()
    {
        Destroy(gameObject);
    }
}
