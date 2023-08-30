using Quantum;
using UnityEngine;
using ZAMB.Settings;
using ZAMB.Utilities;
using static ZAMB.Settings.Consts;

namespace ZAMB.Entities.PlayerScripts
{
    internal unsafe class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator[] animators;
        [SerializeField] private PlayerSettings settings;
        [SerializeField] private float runThreshold = .5f;

        private BoolChangeTrigger _groundedTrigger;

        private PlayerRef _playerRef = default;
        private EntityRef _entityRef = default;
        private Vector3 _velocity;

        internal void Init(EntityRef entityRef, PlayerRef playerRef)
        {
            _groundedTrigger = new();

            _entityRef = entityRef;
            _playerRef = playerRef;

            QuantumEvent.Subscribe<EventStartAiming>(this, StartAiming);
            QuantumEvent.Subscribe<EventStopAiming>(this, StopAiming);

            enabled = true;
        }

        private void OnEnable() => _groundedTrigger.Subscribe(SetGrounded);
        private void OnDisable() => _groundedTrigger.Unsubscribe(SetGrounded);

        private void Update()
        {
            var kcc = QuantumRunner.Default.Game.Frames.Verified.Unsafe.GetPointer<CharacterController3D>(_entityRef);

            _velocity = kcc->Velocity.ToUnityVector3();

            _groundedTrigger.CheckChanges(kcc->Grounded);

            if (_groundedTrigger.Value && _velocity.magnitude > 0f)
            {
                if (_velocity.magnitude < runThreshold)
                    Walk();
                else
                    Run();
            }
            else
                Stop();
        }

        private void SetGrounded(bool groundValue)
        {
            foreach (Animator animator in animators)
                animator.SetBool(Grounded, groundValue);
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
