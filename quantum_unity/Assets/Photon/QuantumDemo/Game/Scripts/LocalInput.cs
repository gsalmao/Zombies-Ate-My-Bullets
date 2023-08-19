using System;
using Photon.Deterministic;
using Quantum;
using UnityEngine;

public class LocalInput : MonoBehaviour
{

    private Controls _controls;

    private void Awake() => _controls = new();

    private void OnEnable()
    {
        QuantumCallback.Subscribe(this, (CallbackPollInput callback) => PollInput(callback));
        _controls = new();
        _controls.Enable();
    }

    private void OnDisable() => _controls.Disable();

    public void PollInput(CallbackPollInput callback)
    {
        Quantum.Input i = new Quantum.Input();

        i.Jump = _controls.Gameplay.Jump.IsPressed();
        i.Direction = _controls.Gameplay.Move.ReadValue<Vector2>().ToFPVector2();

        callback.SetInput(i, DeterministicInputFlags.Repeatable);
    }
}
