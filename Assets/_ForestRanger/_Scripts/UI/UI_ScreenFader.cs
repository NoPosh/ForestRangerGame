using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UI_ScreenFader : MonoBehaviour
{
    //Затемнение экрана
    [Header("Components")]
    [SerializeField] private CanvasGroup _canvasGroup;

    [Header("Settings")]
    [SerializeField] private float _defaultDuration = 1f;
    [SerializeField] private Ease _fadeEase = Ease.InOutQuad;

    private Tween _currentFadeTween;

    private void Reset()
    {
        // Автоматически подтягиваем компонент при добавлении скрипта в редакторе
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// Плавное появление экрана (прозрачность из 1 в 0). Экран становится прозрачным.
    /// </summary>
    public async UniTask FadeInAsync(float? duration = null, CancellationToken token = default)
    {
        await PlayFadeAsync(0f, duration ?? _defaultDuration, token);

        // После исчезновения черного экрана отключаем перехват кликов, 
        // чтобы игрок мог взаимодействовать с миром/UI под фейдером
        _canvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// Плавное затемнение экрана (прозрачность из 0 в 1). Экран становится полностью черным.
    /// </summary>
    public async UniTask FadeOutAsync(float? duration = null, CancellationToken token = default)
    {
        // Перед началом затемнения блокируем все клики, чтобы игрок не прокликал ничего лишнего
        _canvasGroup.blocksRaycasts = true;

        await PlayFadeAsync(1f, duration ?? _defaultDuration, token);
    }

    /// <summary>
    /// Мгновенно устанавливает экран в черное состояние
    /// </summary>
    public void SetBlack()
    {
        KillCurrentTween();
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// Мгновенно делает экран прозрачным
    /// </summary>
    public void SetClear()
    {
        KillCurrentTween();
        _canvasGroup.alpha = 0f;
        _canvasGroup.blocksRaycasts = false;
    }

    private async UniTask PlayFadeAsync(float targetAlpha, float duration, CancellationToken token)
    {
        KillCurrentTween();

        // Создаем DOTween анимацию и превращаем её в UniTask с поддержкой отмены
        _currentFadeTween = _canvasGroup.DOFade(targetAlpha, duration)
            .SetEase(_fadeEase)
            .SetUpdate(true); // Используем unscaled time, чтобы фейдер работал даже при паузе (Time.timeScale = 0)

        /*
        try
        {
            await _currentFadeTween.WithCancellation(token);
        }
        catch (OperationCanceledException)
        {
            // Очищаем твин, если таска была отменена извне
            KillCurrentTween();
            throw;
        }
        */
    }

    private void KillCurrentTween()
    {
        if (_currentFadeTween != null && _currentFadeTween.IsActive())
        {
            _currentFadeTween.Kill();
        }
    }

    private void OnDestroy()
    {
        KillCurrentTween();
    }
}
