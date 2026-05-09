using UnityEngine;
using Zenject;

public class CharacterRotation : IFixedTickable
{
    //Поворачивается либо в сторону движения, либо в сторону правого стика
    private InputProvider _input;
    private CharacterRotationSO _config;
    private Rigidbody2D _rb;

    private float _targetRotation;
    public CharacterRotation(InputProvider input, Rigidbody2D rb, CharacterRotationSO config)
    {
        _input = input;
        _rb = rb;
        _config = config;
    }

    private void SetTargetRotation()
    {
        if (_input.Gameplay_RightJoystick.CurrentValue != Vector2.zero)
        {
            CalculateRotation(_input.Gameplay_RightJoystick.CurrentValue);
            return;
        }
        if (_input.Gameplay_LeftJoystick != Vector2.zero)
            CalculateRotation(_input.Gameplay_LeftJoystick);        
    }

    private void CalculateRotation(Vector2 direction)
    {
        _targetRotation = Vector2.SignedAngle(Vector2.up, direction);
    }

    public void FixedTick()
    {
        SetTargetRotation();
        float newRot = Mathf.MoveTowardsAngle(_rb.rotation, _targetRotation, Time.fixedDeltaTime * _config.RotationSpeed);
        _rb.MoveRotation(newRot);
    }
}
