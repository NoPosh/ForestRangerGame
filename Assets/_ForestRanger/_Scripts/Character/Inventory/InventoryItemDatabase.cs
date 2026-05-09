using UnityEngine;

[CreateAssetMenu(fileName ="ItemDataabse", menuName ="Database/InventoryItems")]
public class InventoryItemDatabase : ScriptableObject
{
    public InventoryItemData[] InventoryItems;
}
