using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    [SerializeField] private Dictionary<ToolType, int> toolsInventory = new Dictionary<ToolType, int>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        // ДЛЯ ТЕСТА: Начальные предметы
        AddTool(ToolType.Bandage, 1);
        AddTool(ToolType.Antiseptic, 2);
        AddTool(ToolType.Tweezers, 1);
        AddTool(ToolType.Ointment, 2);
        AddTool(ToolType.Painkiller, 1);
    }

    public void AddTool(ToolType tool, int amount)
    {
        if (toolsInventory.ContainsKey(tool)) toolsInventory[tool] += amount;
        else toolsInventory.Add(tool, amount);
    }

    public bool HasTool(ToolType tool)
    {
        return toolsInventory.ContainsKey(tool) && toolsInventory[tool] > 0;
    }
    public int GetToolCount(ToolType tool)
    {
        if (toolsInventory.ContainsKey(tool))
        {
            return toolsInventory[tool];
        }
        return 0;
    }

    public bool HasAnyTools()
    {
        foreach (var count in toolsInventory.Values)
        {
            if (count > 0) return true;
        }
        return false;
    }
    public bool HasMinimumTools()
    {
        bool hasBandage = GetToolCount(ToolType.Bandage) >= 1;
        bool hasAntiseptic = GetToolCount(ToolType.Antiseptic) >= 2;
        bool hasTweezers = GetToolCount(ToolType.Tweezers) >= 1;
        bool hasOintment = GetToolCount(ToolType.Ointment) >= 2;
        bool hasPainkiller = GetToolCount(ToolType.Painkiller) >= 1;
        return hasBandage && hasAntiseptic && hasTweezers && hasOintment && hasPainkiller;
    }

    public void ConsumeTool(ToolType tool)
    {
        if (HasTool(tool))
        {
            toolsInventory[tool]--;
            Debug.Log($"Использован {tool}. Осталось {toolsInventory[tool]}");
        }
    }
}
