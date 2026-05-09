using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IState
{
    public UniTask Enter(GameStateMachine stateMachine);
    public UniTask Exit();
}
