using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _animator.SetFloat("Speed", _rb.linearVelocity.magnitude);
    }
}
