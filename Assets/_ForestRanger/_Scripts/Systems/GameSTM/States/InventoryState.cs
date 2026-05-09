using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

public class InventoryState: IState
{
    //Короче видимо тут лучше отправлять все через сигналы
    private GameStateMachine _stateMachine;

    public event Action OnInventoryOpen;    //Для UI_Inventory
    public event Action OnInventoryClose;

    public async UniTask Enter(GameStateMachine stateMachine)
    {
        OnInventoryOpen?.Invoke();
        _stateMachine = stateMachine;

        //По сути можно ждать фидбек от выбора и после этого запускать gameplayState
        await UniTask.CompletedTask;
    }

    public void CloseInventory()
    {
        _stateMachine.SwitchState<GameplayState>().Forget();
    }

    public async UniTask Exit()
    {
        OnInventoryClose?.Invoke();
        await UniTask.CompletedTask; // Если нет асинхронщины, просто возвращаем Task
    }
}
