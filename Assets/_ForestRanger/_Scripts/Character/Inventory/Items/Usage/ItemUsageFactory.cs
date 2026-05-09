using UnityEngine;
using Zenject;

public class ItemUsageFactory
{
    private readonly DiContainer _container;
    private readonly Transform _transform;

    public ItemUsageFactory(DiContainer container, Transform transform)
    {
        _container = container;
        _transform = transform;
    }

    public IItemUsage Create(InventoryItemData itemData)
    {
        return itemData.UsageType switch
        {
            ItemUsageType.Attack => _container.Instantiate<MeleeAttackUsage>(new object[] { _transform, itemData }),
            _ => null
        };
    }
}
