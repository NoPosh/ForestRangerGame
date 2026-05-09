using UnityEngine;
using Zenject;

public class CharacterInstaller : MonoInstaller
{
    [SerializeField] private InventoryItemDatabase _inventoryItemsDatabase;
    [SerializeField] private CharacterMovementSO _movementSO;
    [SerializeField] private CharacterRotationSO _rotationSO;

    [SerializeField] private InteractionDetector _interactionDetector;

    [SerializeField] private Transform _characterTransform;
    [SerializeField] private Rigidbody2D _rigidbody;
    public override void InstallBindings()
    {
        Container.Bind<InventoryItemDatabase>().FromScriptableObject(_inventoryItemsDatabase).AsSingle();
        Container.Bind<CharacterMovementSO>().FromScriptableObject(_movementSO).AsSingle();
        Container.Bind<CharacterRotationSO>().FromScriptableObject(_rotationSO).AsSingle();
        Container.Bind<Transform>().FromInstance(_characterTransform).AsSingle();
        Container.Bind<Rigidbody2D>().FromInstance(_rigidbody).AsSingle();

        Container.Bind<ItemUsageFactory>().AsSingle();

        Container.BindInterfacesAndSelfTo<CharacterMovement>().AsSingle();
        Container.BindInterfacesAndSelfTo<CharacterRotation>().AsSingle();

        Container.Bind<CharacterInventory>().AsSingle().NonLazy();

        Container.Bind<InteractionDetector>().FromInstance(_interactionDetector).AsSingle();
        Container.BindInterfacesAndSelfTo<InteractionController>().AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<ItemUser>().AsSingle().NonLazy();
    }
}