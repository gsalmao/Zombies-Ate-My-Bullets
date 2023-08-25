using System;
using Photon.Deterministic;
using Quantum;
using UnityEngine;

public class LocalInput : MonoBehaviour
{
    private Camera _camera;
    private Controls _controls;

    private void Awake()
    {
        _camera = Camera.main;
        _controls = new();
    }

    private void OnEnable()
    {
        QuantumCallback.Subscribe(this, (CallbackPollInput callback) => PollInput(callback));
        _controls = new();
        _controls.Enable();
    }

    private void OnDisable() => _controls.Disable();

    private void PollInput(CallbackPollInput callback)
    {
        Quantum.Input i = new Quantum.Input();

        i.Aim = _controls.Gameplay.Aim.IsPressed();
        i.Run = _controls.Gameplay.Run.IsPressed();
        i.Shoot = _controls.Gameplay.Shoot.IsPressed();
        i.Jump = _controls.Gameplay.Jump.IsPressed();
        i.Direction = CalculateDirection(_controls.Gameplay.Move.ReadValue<Vector2>()).ToFPVector2();

        callback.SetInput(i, DeterministicInputFlags.Repeatable);
    }

    private Vector3 CalculateDirection(Vector2 input)
    {
        Vector3 forward = Vector3.Normalize(Vector3.ProjectOnPlane(_camera.transform.forward, Vector3.up));
        Vector3 right = Vector3.Normalize(Vector3.ProjectOnPlane(_camera.transform.right, Vector3.up));

        return Vector3.Normalize((input.x * right) + (input.y * forward));
    }
}
