using R3;
using System;
using UnityEngine;
using Zenject;

public class EquipmentHandler : IInitializable, IDisposable
{
    //Отвечает за спавн предмета в руках
    private CharacterInventory _inventory;

    private DisposableBag _bag;

    public EquipmentHandler(CharacterInventory inventory)
    {
        _inventory = inventory;
    }

    public void Initialize()
    {
        //Подписываемся на смену предмета у руках
    }

    private void EquipItem()
    {
        //Спавн предмета в руках
    }

    public void Dispose()
    {

    }
}
