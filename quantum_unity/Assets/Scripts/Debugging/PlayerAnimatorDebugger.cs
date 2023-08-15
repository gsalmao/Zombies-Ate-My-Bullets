using EditorUtilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZAMB.Debugging
{
    internal class PlayerAnimatorDebugger : BaseDebugger
    {
        [SerializeField] private float animatorSpeed = 1f;

        private Animator _animator;

        private const string moveValue = "MoveValue";
        private const string toStandard = "ToStandard";
        private const string toRifle = "ToRifle";
        private const string rifle_Aim = "Rifle_Aim";

        protected override void Awake()
        {
            base.Awake();
            _animator = GetComponentInChildren<Animator>();
        }


        private void Update() => _animator.SetFloat(moveValue, (Mathf.Sin(Time.time * animatorSpeed) + 1) / 2);

        [Button("Get Rifle")]
        private void GetRifle() => _animator.SetTrigger(toRifle);

        [Button("Lose guns")]
        private void LoseGuns() => _animator.SetTrigger(toStandard);

        [Button("Aim")]
        private void Aim() => _animator.Play(rifle_Aim);


    }
}
