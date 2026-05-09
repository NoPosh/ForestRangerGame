using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using R3;
using System;

public class ItemUser: IInitializable, ITickable, IDisposable
{
    private readonly InputProvider _input;
    private CharacterInventory _inventory;
    private ItemUsageFactory _usageFactory;

    private IItemUsage _currentUsage;
    private bool _canUse = true;

    private float _currentCooldown = 0f;

    private DisposableBag _bag;

    public ItemUser(InputProvider input, CharacterInventory inventory, ItemUsageFactory usageFactory)
    {
        _input = input;
        _inventory = inventory;
        _usageFactory = usageFactory;
    }

    public void Initialize()
    {
        _input.Gameplay_RightJoystick.
            Where(dir => dir.sqrMagnitude > 0.25f)
            .Subscribe(async _ => await TryUse()).AddTo(ref _bag);
    }

    public void Tick()
    {
        if (_currentCooldown > 0f) _currentCooldown -= Time.deltaTime;
    }

    private async UniTask TryUse()
    {
        //Еще надо учесть кд
        if (!_canUse) return;
        if (_currentCooldown > 0f) return;
        if (_inventory.SelectedItem.CurrentValue == null) return;       
        if (_inventory.SelectedItem.CurrentValue.UsageType == ItemUsageType.None) return;

        //В зависимости от типа использования выполняем разную логику
        InventoryItemData currentData = _inventory.SelectedItem.CurrentValue;

        //Аттакуем - урон по небольшой области перед персонажем
        _currentUsage = _usageFactory.Create(currentData);
        
        _canUse = false;

        if (_currentUsage != null)
        {
            _currentCooldown = currentData.UseCooldown;
            await _currentUsage.Use();
        }
            

        _canUse = true;
        _currentUsage = null;
    }

    public void Dispose()
    {
        _bag.Dispose();
    }

}
