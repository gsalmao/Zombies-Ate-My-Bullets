using GameInput;
using UnityEngine;

namespace ZAMB.PlayerScripts.PlayerController
{
    /// <summary>
    /// Finite State Machine used to control Player's behaviour. Also, control behaviour of multi-state data, like the character controller's gravity.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        public PlayerState CurrentState { get; private set; }

        [SerializeField] private PlayerReferences playerReferences;

        internal Controls controls;
        internal Vector3 currentGravity;

        private float _gravityForce;

        private void OnEnable()
        {
            controls = new();
            controls.Enable();

            _gravityForce = playerReferences.Settings.GravityForce;

            playerReferences.Init();
            CurrentState = new PlayerState_Standard(this, playerReferences);
            CurrentState.EnterState();
        }

        private void OnDisable() => controls.Disable();

        private void FixedUpdate()
        {
            CurrentState.UpdateState();

            currentGravity = playerReferences.CharacterController.isGrounded ?
                Vector3.down : currentGravity + Vector3.up * _gravityForce * Time.fixedDeltaTime;
        }

        internal void ChangeState(PlayerState nextState)
        {
            CurrentState.ExitState();
            CurrentState = nextState;
            CurrentState.EnterState();
        }
    }
}
