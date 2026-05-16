using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestTrigger : MonoBehaviour
{
    [Header("Панели UI")]
    public GameObject mainMenuPanel;
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    [Header("Кнопки запуска")]
    public GameObject startTreatmentButton;

    [Header("Мини-игры")]
    public GameObject treatmentGamePanel;
    public GameObject emptyGamePanel;
    private MiniGameBase currentActiveGame;

    public void StartTreatmentGame()
    {
        if (InventoryManager.Instance != null && !InventoryManager.Instance.HasAnyTools() || !InventoryManager.Instance.HasMinimumTools())
        {
            ShowNotification("У вас недостаточно инструментов с собой для лечения животного!");
            return;
        }
        StartGame(treatmentGamePanel);
    }

    public void StartEmptyGame()
    {
        StartGame(emptyGamePanel);
    }

    private void StartGame(GameObject gamePanel)
    {
        //mainMenuPanel.SetActive(false);
        resultPanel.SetActive(false);
        gamePanel.SetActive(true);
        currentActiveGame = gamePanel.GetComponentInChildren<MiniGameBase>(true);
        if (currentActiveGame == null)
        {
            return;
        }
        currentActiveGame.OnGameFinished += ShowResult;
    }
    private void ShowNotification(string message)
    {
        if (resultPanel != null)
        {
            resultPanel.SetActive(true);
            if (resultText != null)
            {
                resultText.text = message;
            }
        }
    }

    private void ShowResult(MiniGameResult result)
    {
        Debug.Log("Результат - " + result);
        if (currentActiveGame != null)
        {
            currentActiveGame.OnGameFinished -= ShowResult;
            currentActiveGame.gameObject.SetActive(false);
        }
        if (resultPanel != null)
        {
            resultPanel.SetActive(true);
        }
        if (resultText != null)
        {
            resultText.text = "Результат - " + result.ToString();
        }
    }

    public void CloseResultPanel()
    {
        resultPanel.SetActive(false);
        //mainMenuPanel.SetActive(true);
    }
}
