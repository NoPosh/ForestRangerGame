using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class MiniGameBase : MonoBehaviour, IUIWindow
{
    public Action<MiniGameResult> OnGameFinished;
    public virtual void Open()
    {
        gameObject.SetActive(true);
        OnGameStarted();
    }
    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
    protected virtual void OnGameStarted() { }
    protected void FinishGame(MiniGameResult result)
    {
        OnGameFinished?.Invoke(result);
        Close();
    }
}
