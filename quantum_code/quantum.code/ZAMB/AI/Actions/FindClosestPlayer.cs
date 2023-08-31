using Photon.Deterministic;
using System;

namespace Quantum
{
    [Serializable]
    public unsafe partial class FindClosestPlayer : AIAction
    {
        public AIBlackboardValueKey targetKey;

        public override void Update(Frame frame, EntityRef entity, ref AIContext aiContext)
        {
            var bbComponent = frame.Unsafe.GetPointer<AIBlackboardComponent>(entity);

            var transform = frame.Get<Transform3D>(entity);
            var players = frame.Filter<CharacterController3D, Transform3D>();
            var pathfinder = frame.Get<NavMeshPathfinder>(entity);

            var navmeshGuid = pathfinder.NavMeshGuid;
            var navMesh = frame.FindAsset<NavMesh>(navmeshGuid);

            FP shortestDistance = FP._0;

            while (players.Next(out var e, out var kcc, out var t))
            {
                FP thisDistance = FPVector3.Distance(transform.Position, t.Position);

                if (thisDistance < shortestDistance || shortestDistance == FP._0)
                {
                    shortestDistance = thisDistance;

                    bbComponent->Set(frame, targetKey.Key, e);
                    pathfinder.SetTarget(frame, t.Position, navMesh);
                }
            }

            frame.Set(entity, pathfinder);
        }

    }
}