using R3;
using System;
using UnityEngine;
using Zenject;

public class EquipmentHandler : IDisposable
{
    //Отвечает за спавн предмета в руках
    private CharacterInventory _inventory;
    private SpriteRenderer _itemRender;

    private DisposableBag _bag;

    public EquipmentHandler(CharacterInventory inventory, SpriteRenderer renderer)
    {
        _inventory = inventory;
        _itemRender = renderer;
        _inventory.SelectedItem.Subscribe(_ => UpdateRenderer()).AddTo(ref _bag);
    }
    private void UpdateRenderer()
    {
        if (_inventory.SelectedItem.CurrentValue != null && _inventory.SelectedItem.CurrentValue.Icon != null)
        {
            EquipItem(_inventory.SelectedItem.CurrentValue.Icon);
        }
        else
        {
            OffItem();
        }
    }

    private void EquipItem(Sprite itemIcon)
    {
        _itemRender.sprite = itemIcon;
        _itemRender.gameObject.SetActive(true);
    }

    private void OffItem()
    {
        _itemRender.gameObject.SetActive(false);
    }

    public void Dispose()
    {
        _bag.Dispose();
    }
}
