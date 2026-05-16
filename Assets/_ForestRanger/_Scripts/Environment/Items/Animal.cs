using R3;
using System;
using UnityEngine;
using Zenject;

public class Animal : MonoBehaviour, IInteractable
{
    [SerializeField] private TestTrigger _test;

    public Transform Transform => transform;

    public event Action<IInteractable> OnDestroyed;

    private bool _canIntercat = true;

    public bool CanInteract()
    {
        return _canIntercat;
    }

    public void Interact()
    {
        _test.StartTreatmentGame();
    }

    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }
}
