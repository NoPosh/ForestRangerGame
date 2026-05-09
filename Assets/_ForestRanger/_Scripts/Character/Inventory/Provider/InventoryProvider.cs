using R3;
using UnityEngine;

public class InventoryProvider : IInventoryProvider
{
    private readonly ReactiveProperty<CharacterInventory> _inventory = new();
    public ReadOnlyReactiveProperty<CharacterInventory> Inventory => _inventory;
    public void SetInventory(CharacterInventory inventory) => _inventory.Value = inventory;
}
