using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UI_Inventory : MonoBehaviour
{
    //Открыть/закрыть панель
    //По сути, сейчас у нас инвентарь всегда одинаковый
    [Inject] private IInventoryProvider _inventoryProvider;
    [Inject] private InventoryState _inventoryState;

    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private Button _closeButton;

    [Header("Кнопки предметов")]
    [SerializeField] private List<ItemSelectButton> _itemButtons;

    private CharacterInventory _inventory;

    private void Start()
    {
        _inventoryState.OnInventoryOpen += OpenInventory;
        _inventoryState.OnInventoryClose += CloseInventory;

        _inventoryPanel.SetActive(false);

        if (_inventory == null) _inventory = _inventoryProvider.Inventory.CurrentValue;

        foreach (var item in _itemButtons)
        {
            item.button.onClick.AddListener(() => { _inventory.SelectItem(item.inventoryItemData); _inventoryState.CloseInventory(); });
        }
    }

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(_inventoryState.CloseInventory);
    }
    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(_inventoryState.CloseInventory);
    }

    public void OpenInventory()
    {
        _inventoryPanel.SetActive(true);
    }

    public void CloseInventory()
    {
        _inventoryPanel.SetActive(false);
    }
}

[System.Serializable]
public struct ItemSelectButton
{
    public Button button;
    public InventoryItemData inventoryItemData;
}
