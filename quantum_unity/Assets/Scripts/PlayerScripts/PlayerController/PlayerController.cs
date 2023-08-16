using GameInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZAMB.PlayerScripts.PlayerController
{
    /// <summary>
    /// Finite State Machine used to control Player's behaviour.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        public PlayerState CurrentState { get; private set; }

        [SerializeField] private PlayerReferences playerReferences;

        internal Controls controls;

        private void OnEnable()
        {
            controls = new();
            controls.Enable();
            playerReferences.Init();
            CurrentState = new PlayerState_Standard(this, playerReferences);
            CurrentState.EnterState();
        }

        private void OnDisable() => controls.Disable();

        private void FixedUpdate() => CurrentState.UpdateState();

        internal void ChangeState(PlayerState nextState)
        {
            CurrentState.ExitState();
            CurrentState = nextState;
            CurrentState.EnterState();
        }
    }
}
