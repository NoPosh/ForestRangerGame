using DG.Tweening;
using UnityEngine;

public class UI_PopupEnable : MonoBehaviour
{
    [SerializeField] private float duration = 0.3f;

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;

        transform.DOScale(1f, duration)
            .SetEase(Ease.OutBack);
    }

    private void OnDisable()
    {
        transform.DOScale(0f, duration)
            .SetEase(Ease.OutBack);
    }
}
