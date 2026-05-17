using UnityEngine;
using DG.Tweening;

public class UIFloatingAnimation : MonoBehaviour
{
    [SerializeField] private float moveAmount = 10f;
    [SerializeField] private float duration = 1.2f;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.localPosition;

        transform.DOLocalMoveY(startPos.y + moveAmount, duration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
}