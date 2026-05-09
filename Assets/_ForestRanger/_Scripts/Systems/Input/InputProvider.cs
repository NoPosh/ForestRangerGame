using R3;
using System;
using UnityEngine;
using Zenject;

public class InputProvider: ITickable, IDisposable
{
    private GameControls _control;

    public Vector2 Gameplay_LeftJoystick {  get; private set; }

    private readonly ReactiveProperty<Vector2> _gameplay_RightJoystick = new();
    public ReadOnlyReactiveProperty<Vector2> Gameplay_RightJoystick => _gameplay_RightJoystick;

    private Subject<Unit> _gameplay_Interaction = new Subject<Unit>();
    public Observable<Unit> Gameplay_Interaction => _gameplay_Interaction;

    public InputProvider()
    {
        _control = new GameControls();
        _control.Enable();
        OffInput();

        InitGameplayInput();

        Debug.Log("InputProvider инициализирован");
    }

    private void InitGameplayInput()
    {
        _control.Gameplay.LeftStick.performed += ctx => Gameplay_LeftJoystick = ctx.ReadValue<Vector2>();
        _control.Gameplay.RightStick.performed += ctx => _gameplay_RightJoystick.Value = ctx.ReadValue<Vector2>();

        _control.Gameplay.SouthButton.performed += ctx => _gameplay_Interaction.OnNext(Unit.Default);
    }

    private void OffInput()
    {
        _control.Gameplay.Disable();
    }

    public void ToggleGameplayInput(bool isActive)
    {
        if (isActive)
            _control.Gameplay.Enable();
        else
            _control.Gameplay.Disable();
    }

    public void Tick()
    {

    }

    public void Dispose()
    {
    }
}
