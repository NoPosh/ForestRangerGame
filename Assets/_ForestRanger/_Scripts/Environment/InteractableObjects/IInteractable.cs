using System;
using UnityEngine;

public interface IInteractable
{
    public Transform Transform { get; }

    public bool CanInteract();
    public void Interact();

    public event Action<IInteractable> OnDestroyed;
}
