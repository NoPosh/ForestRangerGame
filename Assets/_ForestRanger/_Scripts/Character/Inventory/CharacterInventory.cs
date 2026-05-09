using R3;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharacterInventory
{
    //Инвентарь - предметы, которые можно брать в руки
    public readonly ReactiveProperty<List<InventoryItemData>> Items = new();

    private ReactiveProperty<InventoryItemData> _selectedItem = new();
    public ReadOnlyReactiveProperty<InventoryItemData> SelectedItem => _selectedItem;

    public CharacterInventory(IInventoryProvider inventoryProvider, InventoryItemDatabase database)
    {
        inventoryProvider.SetInventory(this);
        Items.Value = new List<InventoryItemData>();    //Надо инициализировать все предметы

        foreach(var item in database.InventoryItems)
        {
            Items.Value.Add(item);
        }
    }

    public void AddItem(InventoryItemData item)
    {
        Items.Value.Add(item);
        Items.ForceNotify();
    }

    public void SelectItem(InventoryItemData item)
    {
        if (item == null)
        {
            Debug.LogError("Выбранный айтем null");
            return;
        }

        if (Items.Value.Contains(item))
        {
            _selectedItem.Value = item;
            Debug.Log($"Выбран новый айтем {item.Name}");
        }
        else
        {
            Debug.Log("Такого вредмета нет в инвентаре");
        }
    }
}
