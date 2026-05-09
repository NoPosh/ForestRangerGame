using R3;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    private List<IInteractable> _interactables = new();
    private readonly ReactiveProperty<IInteractable> _closestInteractable = new();

    public ReadOnlyReactiveProperty<IInteractable> ClosestInteractable => _closestInteractable;

    private void Start()
    {
        Observable.Interval(System.TimeSpan.FromSeconds(0.2))
            .Subscribe(_ => UpdateClosest())
            .AddTo(this);
    }

    private void UpdateClosest()
    {
        if (_interactables.Count == 0)
        {
            if (_closestInteractable.Value != null) _closestInteractable.Value = null;
            return;
        }

        IInteractable bestTarget = null;
        float closestSqrDist = float.MaxValue;
        Vector3 currentPos = transform.position;

        foreach (var interactable in _interactables)
        {
            if (!interactable.CanInteract()) continue;

            float sqrDist = (currentPos - interactable.Transform.position).sqrMagnitude;
            if (sqrDist < closestSqrDist)
            {
                closestSqrDist = sqrDist;
                bestTarget = interactable;
            }
        }

        _closestInteractable.Value = bestTarget;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            AddInteractable(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            RemoveInteractable(interactable);
        }
    }

    private void AddInteractable(IInteractable interactable)
    {
        _interactables.Add(interactable);
        interactable.OnDestroyed += RemoveInteractable;
    }

    private void RemoveInteractable(IInteractable interactable)
    {
        interactable.OnDestroyed -= RemoveInteractable;
        _interactables.Remove(interactable);

        if (_closestInteractable.Value == interactable)
        {
            UpdateClosest();
        }
    }
}
