using UnityEngine;


public abstract class InventoryItemData : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public GameObject Prefab;   //Что воявляется в руках

    [Header("Использование")]
    public ItemUsageType UsageType;

    [Header("Атака")]
    public int Damage;
    public float UseCooldown;

}
