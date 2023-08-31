using System;


namespace Quantum
{
    [Serializable]
    public unsafe partial class UpdateTargetPosition : AIAction
    {
        public AIBlackboardValueKey targetKey;

        public override void Update(Frame frame, EntityRef entity, ref AIContext aiContext)
        {
            var bbComponent = frame.Unsafe.GetPointer<AIBlackboardComponent>(entity);
            var target = bbComponent->GetEntityRef(frame, targetKey.Key);

            var pathfinder = frame.Get<NavMeshPathfinder>(entity);
            var targetTransform = frame.Get<Transform3D>(target);
            var navMesh = frame.FindAsset<NavMesh>(pathfinder.NavMeshGuid);

            pathfinder.SetTarget(frame, targetTransform.Position, navMesh);

            frame.Set(entity, pathfinder);
        }
    }
}