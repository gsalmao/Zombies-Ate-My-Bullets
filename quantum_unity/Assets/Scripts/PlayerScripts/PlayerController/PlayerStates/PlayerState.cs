using UnityEngine;

namespace ZAMB.PlayerScripts.PlayerController
{
    public class PlayerState
    {
        protected PlayerController playerController;
        protected PlayerReferences playerReferences;

        public PlayerState(PlayerController playerController, PlayerReferences playerReferences)
        {
            this.playerController = playerController;
            this.playerReferences = playerReferences;
        }

        internal virtual void EnterState() { }
        internal virtual void UpdateState() { }
        internal virtual void ExitState() { }
    }
}
