using System;
using UnityEngine;

namespace ZAMB.Settings
{
    [CreateAssetMenu(fileName = "Player Settings", menuName = "Settings/Player Settings")]
    public class PlayerSettings : ScriptableObject
    {
        [field:SerializeField] public PlayerSettings_Standard Standard { get; private set; }
    }

    [Serializable]
    public class PlayerSettings_Standard
    {
        [field: SerializeField] public float SlowWalkSpeed { get; private set; } = 3f;
        [field: SerializeField] public float FastWalkSpeed { get; private set; } = 7f;
        [field: SerializeField] public float VelocityLerpT { get; private set; } = 0.4f;
        [field: SerializeField] public float TurnSmoothTime { get; private set; } = 0.1f;
        [field: SerializeField] public float AnimationDampTime { get; private set; } = 0.1f;
        [field: SerializeField] public float MaxSlopeAngle { get; private set; } = 46f;
    }
}
