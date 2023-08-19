using UnityEngine;
using ZAMB.Settings;
using Cinemachine;
using UnityEngine.InputSystem;
using static ZAMB.Settings.Consts;
using static ZAMB.PlayerScripts.PlayerController.PlayerUtilities;

namespace ZAMB.PlayerScripts.PlayerController
{
    public class PlayerState_Jumping : PlayerState
    {
        public PlayerState_Jumping(PlayerController playerController, PlayerReferences playerReferences) : base(playerController, playerReferences) { }

        private CharacterController _characterController;
        private Transform _transform;
        private Animator _animator;
        private CinemachineFreeLook _camera;
        private Vector3 _moveDir;
        private Vector2 _input;
        private float _angle;
        private float _turnSmoothVelocity;
        private float _speed;

        internal override void EnterState()
        {
            base.EnterState();
            _speed = references.Settings.Jumping.JumpSpeed;
            _characterController = references.CharacterController;
            _transform = references.Transform;
            _camera = references.PlayerGameplayCam;
            _animator = references.PlayerAnimator;
            _animator.SetBool(Grounded, false);
        }

        internal override void ExitState()
        {
            base.ExitState();
        }

        internal override void UpdateState()
        {
            #region UpdateState

            base.UpdateState();

            //_input = playerController.controls.Gameplay.Move.ReadValue<Vector2>();

            GetMoveDirection(_transform, _camera, _input, references.Settings.TurnTime, ref _moveDir, ref _angle, ref _turnSmoothVelocity);

            if (_input != Vector2.zero)
                _transform.rotation = Quaternion.Euler(0f, _angle, 0f);
            else
                _speed = 0f;

            _characterController.Move(_moveDir * _speed * Time.fixedDeltaTime + playerController.currentGravity);

            #endregion

            #region ChangeStateConditions

            if (_characterController.isGrounded)
                playerController.ChangeState(new PlayerState_Standard(playerController, references));

            #endregion
        }
    }
}
