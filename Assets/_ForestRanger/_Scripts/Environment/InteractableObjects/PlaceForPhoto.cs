using R3;
using System;
using UnityEngine;
using Zenject;

public class PlaceForPhoto : MonoBehaviour, IInteractable
{
    //Сюда если подходит игрок, у него в руках должна быть камера, чтобы взаимодействовать
    [Inject] private IInventoryProvider _inventoryProvider;

    public Transform Transform => transform;

    public event Action<IInteractable> OnDestroyed;

    private Subject<Unit> _onPictureTaked = new();
    public Observable<Unit> OnPictureTaked => _onPictureTaked;

    public bool CanInteract()
    {
        //Можно, только если в руках фотоаппарат
        if (_inventoryProvider.Inventory.CurrentValue.SelectedItem.CurrentValue == null) return false;

        if (_inventoryProvider.Inventory.CurrentValue.SelectedItem.CurrentValue.Name == "Камера")
            return true;

        return false;
    }

    public void Interact()
    {
        //Делаем фотку
        Debug.Log("Сделали фотку");
        _onPictureTaked.OnNext(Unit.Default);
        //Тут надо крч на уровень послать сигнал
    }

    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
        _onPictureTaked.OnCompleted();
        _onPictureTaked.Dispose();
    }
}
