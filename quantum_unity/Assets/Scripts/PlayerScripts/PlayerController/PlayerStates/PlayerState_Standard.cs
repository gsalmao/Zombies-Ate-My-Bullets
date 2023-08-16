using UnityEngine;
using ZAMB.Settings;
using Cinemachine;

namespace ZAMB.PlayerScripts.PlayerController
{
    public class PlayerState_Standard : PlayerState
    {
        public PlayerState_Standard(PlayerController playerController, PlayerReferences playerReferences) : base(playerController, playerReferences) { }

        private PlayerSettings_Standard _settings;
        private Transform transform;
        private Animator animator;
        private CinemachineFreeLook camera;
        private Vector3 moveInputCorrectAxis;
        private Vector3 moveDirection;
        private float turnSmoothVelocity;
        private float targetAngle;
        private float angle;
        private float speed;

        internal override void EnterState()
        {
            base.EnterState();
            transform = playerReferences.Transform;
            _settings = playerReferences.PlayerSettings.Standard;
            camera = playerReferences.PlayerGameplayCam;
            animator = playerReferences.PlayerAnimator;
        }

        internal override void UpdateState()
        {
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

            //rigidbody.value.LerpVelocity(CheckGroundNormal(moveDirection), speed, settings.VelocityLerpT);
        }
        internal override void ExitState()
        {
            base.ExitState();
        }

        private void SetMovementValues()
        {
            Vector2 input = playerController.controls.Gameplay.Move.ReadValue<Vector2>();
            moveInputCorrectAxis = playerController.controls.Gameplay.Move.ReadValue<Vector2>().InputToDir();
            Debug.Log($"Input: {input}");
            Debug.Log($"Move Axis: {moveInputCorrectAxis}");
            Debug.Log($"Target Angle: {targetAngle}");

            targetAngle = Mathf.Atan2(moveInputCorrectAxis.x, moveInputCorrectAxis.z) * Mathf.Rad2Deg + camera.m_XAxis.Value;

            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, _settings.TurnSmoothTime);
            speed = 0;
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
