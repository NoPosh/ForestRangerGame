using R3;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryProvider
{
    public ReadOnlyReactiveProperty<CharacterInventory> Inventory { get; }
    public void SetInventory(CharacterInventory inventory);
}
