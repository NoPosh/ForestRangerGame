using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance;

    [Header("Настройки UI")]
    public Transform popupParent;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartMiniGame(MiniGameBase miniGamePrefab, System.Action<MiniGameResult> onComplete)
    {
        MiniGameBase newGame = Instantiate(miniGamePrefab, popupParent);
        newGame.OnGameFinished += onComplete;
        newGame.Open();
    }
}
