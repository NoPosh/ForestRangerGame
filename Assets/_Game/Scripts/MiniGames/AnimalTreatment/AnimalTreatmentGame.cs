using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class AnimalTreatmentGame : MiniGameBase
{
    [Header("Настройки времени и стресса")]
    public float passiveStressRate = 2f;
    public float maxStress = 100f;

    [Header("UI Элементы")]
    public Slider stressSlider;
    public TextMeshProUGUI descriptionText;
    public Button calmDownButton;
    public Transform woundsContainer;
    public SkillCheckUI skillCheckSystem;
    public Button[] toolButtons;

    private float currentStress = 0f;
    private int mistakesCount = 0;
    private bool isAnimalPanicking = false;
    private bool isGameActive = false;
    private ToolType currentSelectedTool = ToolType.None;
    private List<WoundZone> allWounds = new List<WoundZone>();
    public bool IsPanicking => isAnimalPanicking;

    private void Start()
    {
        allWounds.AddRange(woundsContainer.GetComponentsInChildren<WoundZone>(true));
        foreach (var wound in allWounds) wound.Setup(this);
        calmDownButton.onClick.AddListener(OnCalmDownClicked);
        calmDownButton.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        OnGameStarted();
    }

    protected override void OnGameStarted()
    {
        Debug.Log("Запуск игры");
        currentStress = 0f;
        mistakesCount = 0;
        isAnimalPanicking = false;
        isGameActive = true;
        currentSelectedTool = ToolType.None;
        calmDownButton.gameObject.SetActive(false);
        UpdateToolButtons();
        UpdateStressUI();
        ShowInitialMessage();
    }
    private void ShowInitialMessage()
    {
        string msg = "Выберите инструмент и рану для лечения";
        for (int i = 0; i < toolButtons.Length; i++)
        {
            if (InventoryManager.Instance != null && !InventoryManager.Instance.HasTool((ToolType)i))
            {
                msg += $"\n{(ToolType)i} закончился!";
            }
        }
        ShowWoundDescription("Осмотр", msg);
    }

    public void UpdateToolButtons()
    {
        if (InventoryManager.Instance == null) return;
        for (int i = 0; i < toolButtons.Length; i++)
        {
            if (toolButtons[i] != null)
            {
                bool hasItem = InventoryManager.Instance.HasTool((ToolType)i);
                toolButtons[i].interactable = hasItem;
            }
        }
    }

    public void SelectTool(int toolIndex)
    {
        if (!isGameActive || isAnimalPanicking) return;
        ToolType selected = (ToolType)toolIndex;
        if (InventoryManager.Instance != null && !InventoryManager.Instance.HasTool(selected))
        {
            ShowWoundDescription("Ошибка", $"У вас нет инструмента - {selected}!");
            return;
        }
        currentSelectedTool = selected;
        ShowWoundDescription("Инструмент", $"Выбран - {currentSelectedTool}");
    }

    public void ApplyCorrectTool(ToolType tool)
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.ConsumeTool(tool);
            UpdateToolButtons();
        }
        currentSelectedTool = ToolType.None;
    }

    private void Update()
    {
        if (!isGameActive) return;
        float multiplier = isAnimalPanicking ? 2.0f : 1.0f;
        AddStress(passiveStressRate * multiplier * Time.deltaTime);
    }

    public float GetStressMultiplier()
    {
        return 1f + (currentStress / maxStress) * 1.5f;
    }

    public void OnBleedTimerZero()
    {
        if (!isGameActive) return;
        isGameActive = false;
        FinishGame(MiniGameResult.Failure_Bleed);
    }

    public void AddStress(float amount)
    {
        if (!isGameActive) return;
        currentStress += amount;
        currentStress = Mathf.Clamp(currentStress, 0, maxStress);
        if (currentStress >= maxStress)
        {
            isGameActive = false;
            FinishGame(MiniGameResult.Failure_Stress);
            return;
        }
        if (currentStress >= (maxStress * 0.5f) && !isAnimalPanicking)
        {
            isAnimalPanicking = true;
            calmDownButton.gameObject.SetActive(true);
            ShowWoundDescription("Паника", "Животное напугано! Срочно успокойте его!");
        }
        UpdateStressUI();
    }

    public ToolType GetCurrentSelectedTool() => currentSelectedTool;

    public void ShowWoundDescription(string title, string desc)
    {
        if (descriptionText != null) descriptionText.text = $"<b>{title}</b>\n{desc}";
    }

    public void ApplyWrongTool()
    {
        mistakesCount++;
        AddStress(maxStress * 0.2f);
        ShowWoundDescription("Ошибка", "Неверное действие! Животное дернулось от боли");
    }

    private void OnCalmDownClicked()
    {
        if (!isGameActive) return;
        currentStress -= 5f;
        if (currentStress <= (maxStress * 0.3f))
        {
            isAnimalPanicking = false;
            calmDownButton.gameObject.SetActive(false);
            ShowWoundDescription("Спокойствие", "Животное успокоилось, можно продолжать лечение");
        }
        currentStress = Mathf.Max(0, currentStress);
        UpdateStressUI();
    }

    private void UpdateStressUI()
    {
        if (stressSlider != null) stressSlider.value = currentStress / maxStress;
    }

    public void CheckWinCondition()
    {
        if (!isGameActive) return;

        foreach (var wound in allWounds)
        {
            if (wound.gameObject.activeInHierarchy && !wound.IsHealed())
            {
                return;
            }
        }
        isGameActive = false;
        bool isPerfect = (mistakesCount == 0 && currentStress < (maxStress * 0.2f));
        MiniGameResult result = isPerfect ? MiniGameResult.Success_Perfect : MiniGameResult.Success_Normal;
        FinishGame(result);
    }
}
