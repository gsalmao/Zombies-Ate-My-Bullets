namespace ZAMB.PlayerScripts.PlayerController
{
    public class PlayerState
    {
        protected PlayerController playerController;
        protected PlayerReferences references;

        public PlayerState(PlayerController playerController, PlayerReferences playerReferences)
        {
            this.playerController = playerController;
            this.references = playerReferences;
        }

        internal virtual void EnterState() { }
        internal virtual void UpdateState() { }
        internal virtual void ExitState() { }
    }
}
