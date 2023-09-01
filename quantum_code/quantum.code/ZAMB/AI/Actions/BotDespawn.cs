using Photon.Deterministic;
using System;

namespace Quantum
{
    [Serializable]
    public partial class BotDespawn : AIAction
    {
        public override void Update(Frame frame, EntityRef entity, ref AIContext aiContext)
        {
            frame.Events.BotDespawn(entity);
            frame.Destroy(entity);
        }
    }
}