using UnityEngine;
using DG.Tweening;

public class UIPopupAnimation : MonoBehaviour
{
    [SerializeField] private float duration = 0.3f;

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;

        transform.DOScale(1f, duration)
            .SetEase(Ease.OutBack);
    }
}