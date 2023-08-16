using UnityEngine;
using ZAMB.Settings;
using Cinemachine;

namespace ZAMB.PlayerScripts.PlayerController
{
    public class PlayerState_Standard : PlayerState
    {
        public PlayerState_Standard(PlayerController playerController, PlayerReferences playerReferences) : base(playerController, playerReferences) { }

        private PlayerSettings_Standard _settings;
        private CharacterController _characterController;
        private Transform transform;
        private Animator animator;
        private CinemachineFreeLook camera;
        private Vector3 moveInputCorrectAxis;
        private Vector3 moveDirection;
        private Vector3 gravity;
        private float turnSmoothVelocity;
        private float targetAngle;
        private float angle;
        private float speed;

        internal override void EnterState()
        {
            base.EnterState();
            _settings = playerReferences.PlayerSettings.Standard;
            _characterController = playerReferences.CharacterController;
            transform = playerReferences.Transform;
            camera = playerReferences.PlayerGameplayCam;
            animator = playerReferences.PlayerAnimator;

            gravity = new Vector3(0f, -playerReferences.PlayerSettings.GravityForce, 0f) * Time.fixedDeltaTime;
        }

        internal override void UpdateState()
        {
            if(!_characterController.isGrounded)
            {
                playerController.ChangeState(new PlayerState_Jumping(playerController, playerReferences));
                return;
            }

            base.UpdateState();

            SetMovementValues();

            if (moveInputCorrectAxis != Vector3.zero)
            {
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                if (moveInputCorrectAxis.magnitude < .3f)
                    SlowWalk();
                else
                    FastWalk();
            }
            else
                StopWalk();

            _characterController.Move(moveDirection * speed * Time.fixedDeltaTime + gravity);
        }

        internal override void ExitState()
        {
            base.ExitState();
        }

        private void SetMovementValues()
        {
            Vector2 input = playerController.controls.Gameplay.Move.ReadValue<Vector2>();
            moveInputCorrectAxis = playerController.controls.Gameplay.Move.ReadValue<Vector2>().InputToDir();

            targetAngle = Mathf.Atan2(moveInputCorrectAxis.x, moveInputCorrectAxis.z) * Mathf.Rad2Deg + camera.m_XAxis.Value;

            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, _settings.TurnSmoothTime);
            speed = 0;

            Debug.Log($"MoveDirection: {moveDirection}");
            Debug.Log($"Move Axis: {moveInputCorrectAxis}");
            Debug.Log($"Target Angle: {targetAngle}");
        }

        private void SlowWalk()
        {
            animator.SetFloat("MoveValue", 0.5f, _settings.AnimationDampTime, Time.deltaTime);
            speed = _settings.SlowWalkSpeed;
        }

        private void FastWalk()
        {
            animator.SetFloat("MoveValue", 1f, _settings.AnimationDampTime, Time.deltaTime);
            speed = _settings.FastWalkSpeed;
        }

        private void StopWalk()
        {
            animator.SetFloat("MoveValue", 0f, _settings.AnimationDampTime, Time.deltaTime);
        }
    }
}
