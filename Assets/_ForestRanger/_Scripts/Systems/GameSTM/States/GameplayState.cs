using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameplayState : IState
{
    private InputProvider _inputProvider;

    public GameplayState(InputProvider inputProvider)
    {
        _inputProvider = inputProvider;
    }

    public async UniTask Enter(GameStateMachine stateMachine)
    {
        //_hud.Show();
        _inputProvider.ToggleGameplayInput(true);

        await UniTask.CompletedTask;
    }

    public async UniTask Exit()
    {
        //_hud.Hide();
        _inputProvider.ToggleGameplayInput(false);

        await UniTask.CompletedTask; // Если нет асинхронщины, просто возвращаем Task
    }
}
