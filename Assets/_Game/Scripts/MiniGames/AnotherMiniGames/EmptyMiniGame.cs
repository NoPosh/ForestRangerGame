using UnityEngine;
using UnityEngine.UI;

public class EmptyMiniGame : MiniGameBase
{
    [Header("UI")]
    public Button closeButton;

    private void Start()
    {
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(() => FinishGame(MiniGameResult.Cancelled));
        }
    }
}
