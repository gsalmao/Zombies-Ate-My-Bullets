using Photon.Deterministic;
using Quantum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZAMB.Settings;
using static ZAMB.Settings.Consts;

namespace ZAMB.PlayerScripts
{
    public unsafe class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator[] animators;
        [SerializeField] private PlayerSettings settings;
        [SerializeField] private float runThreshold = .5f;


        private PlayerRef _playerRef = default;
        private EntityRef _entityRef = default;

        private Vector3 _velocity;

        private bool _prevGrounded;
        private bool _grounded;

        public void Init(EntityRef entityRef, PlayerRef playerRef)
        {
            _entityRef = entityRef;
            _playerRef = playerRef;

            QuantumEvent.Subscribe<EventStartAiming>(this, StartAiming);
            QuantumEvent.Subscribe<EventStopAiming>(this, StopAiming);
        }

        private void Update()
        {
            var kcc = QuantumRunner.Default.Game.Frames.Verified.Unsafe.GetPointer<CharacterController3D>(_entityRef);

            _grounded = kcc->Grounded;
            _velocity = kcc->Velocity.ToUnityVector3();

            SetGrounded();

            if (_grounded && _velocity.magnitude > 0f)
            {
                if (_velocity.magnitude < runThreshold)
                    Walk();
                else
                    Run();
            }
            else
                Stop();
        }

        private void SetGrounded()
        {
            if(_grounded != _prevGrounded)
                foreach (Animator animator in animators)
                    animator.SetBool(Grounded, _grounded);

            _prevGrounded = _grounded;
        }
        private void Walk()
        {
            foreach(Animator animator in animators)
                animator.SetFloat(MoveValue, 0.5f, settings.Standard.AnimationDampTime, Time.deltaTime);
        }
        private void Run()
        {
            foreach (Animator animator in animators)
                animator.SetFloat(MoveValue, 1f, settings.Standard.AnimationDampTime, Time.deltaTime);
        }
        private void Stop()
        {
            foreach (Animator animator in animators)
                animator.SetFloat(MoveValue, 0f, settings.Standard.AnimationDampTime, Time.deltaTime);
        }

        private void StartAiming(EventStartAiming e)
        {
            if (e.playerRef != _playerRef)
                return;

            foreach (Animator animator in animators)
                animator.SetBool(Aim, true);
        }

        private void StopAiming(EventStopAiming e)
        {
            if (e.playerRef != _playerRef)
                return;

            foreach (Animator animator in animators)
                animator.SetBool(Aim, false);
        }
    }
}
