using Quantum;
using UnityEngine;
using ZAMB.RiggingUtilities;

namespace ZAMB.Entities.PlayerScripts
{
    /// <summary>
    /// Initialize the Player Entity
    /// </summary>
    public unsafe class PlayerSetup : EntitySetup
    {
        [SerializeField] private PlayerAnimation playerAnimation;
        [SerializeField] private AimConstraintController headAim;

        private PlayerRef _playerRef;

        public override void Init()
        {
            base.Init();

            PlayerLink* playerLink = QuantumRunner.Default.Game.Frames.Verified.Unsafe.GetPointer<PlayerLink>(entityRef);

            _playerRef = playerLink->Player;

            playerAnimation.Init(entityRef, _playerRef);

            if (!QuantumRunner.Default.Game.PlayerIsLocal(_playerRef))
                return;

            var playerCameras = FindObjectOfType<PlayerCameras>();
            
            playerCameras.SetupCameras(transform);

            headAim.Init(playerCameras.LookDirection);
        }
    }
}
