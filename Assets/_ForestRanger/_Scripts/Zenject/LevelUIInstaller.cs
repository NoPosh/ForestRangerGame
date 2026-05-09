using UnityEngine;
using Zenject;

public class LevelUIInstaller : MonoInstaller
{
    [SerializeField] private UI_Inventory _inventory;
    public override void InstallBindings()
    {
        Container.Bind<UI_Inventory>().FromInstance(_inventory).AsSingle();
    }
}