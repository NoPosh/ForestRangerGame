using UnityEngine;
using Zenject;

public class CharacterMovement: IFixedTickable
{
    private InputProvider _input;
    private CharacterMovementSO _config;
    private Rigidbody2D _rb;

    public CharacterMovement(InputProvider input, Rigidbody2D rb, CharacterMovementSO config)
    {
        _input = input;
        _rb = rb;
        _config = config;
    }

    public void FixedTick()
    {
        _rb.linearVelocity = _input.Gameplay_LeftJoystick * _config.MaxSpeed;
    }
}
