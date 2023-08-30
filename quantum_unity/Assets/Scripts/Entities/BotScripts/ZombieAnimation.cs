using Photon.Deterministic;
using Quantum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZAMB.Utilities;
using static ZAMB.Settings.Consts;

namespace ZAMB.Entities.BotScripts
{
    internal unsafe class ZombieAnimation : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private BoolChangeTrigger _walkTrigger;
        private EntityRef _entityRef;

        internal void Init(EntityRef entityRef)
        {
            _walkTrigger = new();
            _entityRef = entityRef;

            enabled = true;
        }

        private void OnEnable() => _walkTrigger.Subscribe(SetWalking);
        private void OnDisable() => _walkTrigger.Unsubscribe(SetWalking);


        private void Update()
        {
            var pathfinder = QuantumRunner.Default.Game.Frames.Verified.Unsafe.GetPointer<NavMeshSteeringAgent>(_entityRef);
            _walkTrigger.Process(pathfinder->CurrentSpeed > FP._0);
        }

        private void SetWalking(bool value) => animator.SetBool(Walking, value);
    }
}
