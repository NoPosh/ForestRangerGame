using UnityEngine;
using Zenject;

public class LevelServicesInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IInventoryProvider>().To<InventoryProvider>().AsSingle();
    }
}