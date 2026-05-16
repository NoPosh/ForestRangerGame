using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WoundZone : MonoBehaviour
{
    [Header("Настройки")]
    public string woundName;
    [TextArea] public string diagnosisHint;
    public List<ToolType> requiredSteps;

    [Header("Кровотечение")]
    public bool isBleeding = false;
    public float bleedTimer = 10f;

    [Header("Ссылки UI")]
    public TextMeshProUGUI timerText;
    public Slider bleedSlider;

    private float currentBleedTime;
    private int currentStepIndex = 0;
    private bool isHealed = false;
    private AnimalTreatmentGame mainGame;

    public void Setup(AnimalTreatmentGame game)
    {
        mainGame = game;
        ResetWound();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }
    public void ResetWound()
    {
        currentBleedTime = bleedTimer;
        currentStepIndex = 0;
        isHealed = false;
        gameObject.SetActive(true);
        if (timerText != null) timerText.gameObject.SetActive(isBleeding);
        if (bleedSlider != null)
        {
            bleedSlider.gameObject.SetActive(isBleeding);
            bleedSlider.maxValue = bleedTimer;
            bleedSlider.value = bleedTimer;
        }
    }

    void Update()
    {
        bool isPaused = mainGame != null && mainGame.skillCheckSystem != null && mainGame.skillCheckSystem.IsActive;
        if (isBleeding && !isHealed && !isPaused)
        {
            currentBleedTime -= Time.deltaTime;
            if (timerText != null) timerText.text = Mathf.Ceil(currentBleedTime).ToString();
            if (bleedSlider != null) bleedSlider.value = currentBleedTime;
            if (currentBleedTime <= 0)
            {
                isBleeding = false;
                mainGame.OnBleedTimerZero();
            }
        }
    }

    private void OnClick()
    {
        if (isHealed) return;
        if (mainGame.IsPanicking)
        {
            mainGame.ShowWoundDescription("Осторожно!", "Сначала успокойте животное!");
            return;
        }
        if (mainGame.skillCheckSystem.IsActive) return;
        ToolType selected = mainGame.GetCurrentSelectedTool();
        if (selected == ToolType.None)
        {
            mainGame.ShowWoundDescription(woundName, diagnosisHint);
            return;
        }
        if (requiredSteps.Count > 0 && selected == requiredSteps[currentStepIndex])
        {
            mainGame.skillCheckSystem.StartCheck(mainGame.GetStressMultiplier(), (success) =>
            {
                if (success)
                {
                    currentStepIndex++;
                    mainGame.ApplyCorrectTool(selected);

                    if (currentStepIndex >= requiredSteps.Count)
                    {
                        HealWound();
                    }
                    else
                    {
                        mainGame.ShowWoundDescription("Успех", "Этап завершен. Выберите следующий инструмент");
                    }
                }
                else
                {
                    mainGame.ApplyWrongTool();
                }
            });
        }
        else
        {
            mainGame.ApplyWrongTool();
        }
    }
    private void HealWound()
    {
        isHealed = true;
        isBleeding = false;
        if (timerText != null) timerText.gameObject.SetActive(false);
        if (bleedSlider != null) bleedSlider.gameObject.SetActive(false);
        gameObject.SetActive(false);
        mainGame.CheckWinCondition();
    }

    public bool IsHealed() => isHealed;
}
