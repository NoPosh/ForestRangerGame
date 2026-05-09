using UnityEngine;
using Zenject;

public class GameStateInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();

        Container.Bind<IState>().To<GameplayState>().AsSingle();
        Container.BindInterfacesAndSelfTo<InventoryState>().AsSingle();
    }
}