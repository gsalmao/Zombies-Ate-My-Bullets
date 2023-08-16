using UnityEngine;
using ZAMB.Settings;
using Cinemachine;
using UnityEngine.InputSystem;
using static ZAMB.Settings.Consts;
using static ZAMB.PlayerScripts.PlayerController.PlayerUtilities;

namespace ZAMB.PlayerScripts.PlayerController
{
    public class PlayerState_Standard : PlayerState
    {
        public PlayerState_Standard(PlayerController playerController, PlayerReferences playerReferences) : base(playerController, playerReferences) { }

        private PlayerSettings_Standard _settings;
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

            playerController.controls.Gameplay.Jump.performed += Jump;

            _settings = references.Settings.Standard;
            _characterController = references.CharacterController;
            _transform = references.Transform;
            _camera = references.PlayerGameplayCam;
            _animator = references.PlayerAnimator;
            _animator.SetBool(Grounded, true);
        }

        internal override void UpdateState()
        {
            #region UpdateState

            base.UpdateState();

            _input = playerController.controls.Gameplay.Move.ReadValue<Vector2>();

            GetMoveDirection(_transform, _camera, _input, references.Settings.TurnTime, ref _moveDir, ref _angle, ref _turnSmoothVelocity);

            if (_input != Vector2.zero)
            {
                _transform.rotation = Quaternion.Euler(0f, _angle, 0f);

                if (_input.magnitude < .3f)
                    SlowWalk();
                else
                    FastWalk();
            }
            else
                StopWalk();

            _characterController.Move(_moveDir * _speed * Time.fixedDeltaTime + playerController.currentGravity);

            #endregion

            #region Change State Conditions

            if (!_characterController.isGrounded)
                playerController.ChangeState(new PlayerState_Jumping(playerController, references));

            #endregion
        }

        internal override void ExitState()
        {
            base.ExitState();
            playerController.controls.Gameplay.Jump.performed -= Jump;
        }

        private void SlowWalk()
        {
            _animator.SetFloat(MoveValue, 0.5f, _settings.AnimationDampTime, Time.deltaTime);
            _speed = _settings.SlowWalkSpeed;
        }
        private void FastWalk()
        {
            _animator.SetFloat(MoveValue, 1f, _settings.AnimationDampTime, Time.deltaTime);
            _speed = _settings.FastWalkSpeed;
        }
        private void StopWalk()
        {
            _animator.SetFloat(MoveValue, 0f, _settings.AnimationDampTime, Time.deltaTime);
            _speed = 0f;
        }
        private void Jump(InputAction.CallbackContext ctx)
        {
            Debug.Log("Jump");
            playerController.currentGravity.y = references.Settings.Jumping.JumpForce;
        }
    }
}
