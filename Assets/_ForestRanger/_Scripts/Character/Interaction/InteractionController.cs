using R3;
using System;
using UnityEngine;
using Zenject;

public class InteractionController: IInitializable, IDisposable
{
    private readonly InputProvider _input;
    private readonly InteractionDetector _detector;
    private DisposableBag _bag;

    public InteractionController(InputProvider input, InteractionDetector detector)
    {
        _input = input;
        _detector = detector;
    }

    public void Initialize()
    {
        _input.Gameplay_Interaction
            .Subscribe(_ => TryInteract())
            .AddTo(ref _bag);
    }

    private void TryInteract()
    {
        var target = _detector.ClosestInteractable.CurrentValue;

        if (target != null && target.CanInteract())
        {
            target.Interact();
            Debug.Log("Взаимодействие");
        }
        else
        {
            Debug.Log("Не с чем взаимодействовать");
        }
    }

    public void Dispose()
    {
        _bag.Dispose();
    }
}
