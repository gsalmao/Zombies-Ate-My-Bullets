using Quantum;
using UnityEngine;
using ZAMB.RiggingUtilities;

namespace ZAMB.PlayerScripts
{
    /// <summary>
    /// Initialize the Player Entity
    /// </summary>
    public unsafe class PlayerSetup : MonoBehaviour
    {
        [SerializeField] private PlayerAnimation playerAnimation;
        [SerializeField] private AimConstraintController headAim;

        private PlayerRef _playerRef;

        public void Init()
        {
            EntityRef entityRef = GetComponent<EntityView>().EntityRef;
            PlayerLink* playerLink = QuantumRunner.Default.Game.Frames.Verified.Unsafe.GetPointer<PlayerLink>(entityRef);

            _playerRef = playerLink->Player;

            playerAnimation.Init(_playerRef, entityRef);

            if (!QuantumRunner.Default.Game.PlayerIsLocal(_playerRef))
                return;

            var playerCameras = FindObjectOfType<PlayerCameras>();
            
            playerCameras.SetupCameras(transform);

            headAim.Init(playerCameras.LookDirection);
        }
    }
}
