using System;
using UnityEngine;

namespace ZAMB.Settings
{
    [CreateAssetMenu(fileName = "Player Settings", menuName = "Settings/Player Settings")]
    public class PlayerSettings : ScriptableObject
    {
        [field: SerializeField] public PlayerSettings_Standard Standard { get; private set; }
        [field: SerializeField] public PlayerSettings_Jumping Jumping { get; private set; }
        [field: SerializeField] public float GravityForce { get; private set; } = -9.8f;
        [field: SerializeField] public float TurnTime { get; private set; } = 0.1f;
    }

    [Serializable]
    public class PlayerSettings_Standard
    {
        [field: SerializeField] public float SlowWalkSpeed { get; private set; } = 3f;
        [field: SerializeField] public float FastWalkSpeed { get; private set; } = 7f;
        [field: SerializeField] public float AnimationDampTime { get; private set; } = 0.1f;
    }
    [Serializable]
    public class PlayerSettings_Aiming
    {

    }
    [Serializable]
    public class PlayerSettings_Jumping
    {
        [field: SerializeField] public float JumpForce { get; private set; } = 20f;
        [field: SerializeField] public float JumpSpeed { get; private set; } = 3f;
    }
    [Serializable]
    public class PlayerSettings_Shooting
    {

    }
    [Serializable]
    public class PlayerSettings_Dieing
    {

    }
}
