using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;
using R3;
using Cysharp.Threading.Tasks;

public class GameStateMachine : IInitializable
{
    private readonly Dictionary<Type, IState> _states = new();
    private readonly ReactiveProperty<IState> _currentState = new();

    public ReadOnlyReactiveProperty<IState> CurrentState => _currentState;

    [Inject]
    public void Construct(List<IState> states)
    {
        foreach (var state in states)
        {
            _states[state.GetType()] = state;
        }
    }
    public void Initialize()
    {
        //Тут можно стартануть с состояния игры Бутстрап или типо того
        SwitchState<GameplayState>().Forget();
    }

    public async UniTask SwitchState<TState>() where TState : IState
    {
        if (_currentState.Value != null)
        {
            await _currentState.Value.Exit();
        }

        if (_states.TryGetValue(typeof(TState), out var state))
        {
            _currentState.Value = state;
            Debug.Log($"Игра перешла в состояние: {_currentState.Value.GetType()}");
            await _currentState.Value.Enter(this);
        }
    }
}
