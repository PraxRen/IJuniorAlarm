using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Building : MonoBehaviour
{
    private bool _isThiefEntered;

    public event Action Enter;
    public event Action Exit;

    private void OnTriggerEnter(Collider other)
    {
        if (_isThiefEntered)
            return;

        if (other.TryGetComponent(out Thief thief) == false)
            return;

        Enter?.Invoke();
        _isThiefEntered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (_isThiefEntered == false)
            return;

        if (other.TryGetComponent(out Thief thief) == false)
            return;

        Exit?.Invoke();
        _isThiefEntered = false;
    }
}
