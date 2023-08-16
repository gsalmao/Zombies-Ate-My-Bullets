using System;
using Cinemachine;
using UnityEngine;
using ZAMB.Settings;

namespace ZAMB.PlayerScripts.PlayerController
{
    /// <summary>
    /// Hold references of the player to be used inside of the FSM. Exposes just a static Transform, for enemies's logic.
    /// </summary>
    [Serializable]
    public class PlayerReferences
    {
        public static Transform ExposedTransform { get; private set; }

        [field:SerializeField] internal Transform Transform { get; private set; }
        [field:SerializeField] internal PlayerSettings PlayerSettings { get; private set; }
        [field: SerializeField] internal CinemachineFreeLook PlayerGameplayCam { get; private set; }
        [field: SerializeField] internal CharacterController CharacterController { get; private set; }
        [field:SerializeField] internal Animator PlayerAnimator { get; private set; }
        [field:SerializeField] internal Animator WeaponAnimator { get; private set; }

        internal void Init() => ExposedTransform = Transform;

    }
}
