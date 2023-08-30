using Quantum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ZAMB.Settings.Consts;

namespace ZAMB.Entities.BotScripts
{
    internal unsafe class ZombieAnimation : MonoBehaviour
    {
        private EntityRef _entityRef;

        private bool _prevWalking;
        private bool _walking;

        internal void Init(EntityRef entityRef)
        {
            _entityRef = entityRef;
            enabled = true;
        }

        private void Update()
        {
            var pathfinder = QuantumRunner.Default.Game.Frames.Verified.Unsafe.GetPointer<NavMeshSteeringAgent>(_entityRef);
            //pathfinder->CurrentSpeed
        }
    }
}
