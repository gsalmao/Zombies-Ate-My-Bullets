using Photon.Deterministic;
using System;

namespace Quantum
{
    [Serializable]
    public partial class BotDeath : AIAction
    {
        public override void Update(Frame frame, EntityRef entity, ref AIContext aiContext)
        {
            if(frame.Has<NavMeshPathfinder>(entity))
                frame.Remove<NavMeshPathfinder>(entity);

            if (frame.Has<NavMeshSteeringAgent>(entity))
                frame.Remove<NavMeshSteeringAgent>(entity);

            if (frame.Has<PhysicsBody3D>(entity))
                frame.Remove<PhysicsBody3D>(entity);

            if (frame.Has<PhysicsCollider3D>(entity))
                frame.Remove<PhysicsCollider3D>(entity);

            frame.Events.BotDeath(entity);
        }
    }
}