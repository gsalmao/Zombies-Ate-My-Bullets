using Photon.Deterministic;
using System;

namespace Quantum
{
    [Serializable]
    public partial class TargetAtRangeDecision : HFSMDecision
    {
        public AIBlackboardValueKey targetKey;
        public FP distance;
        public override unsafe bool Decide(Frame frame, EntityRef entity, ref AIContext aiContext)
        {
            var bb = frame.Unsafe.GetPointer<AIBlackboardComponent>(entity);
            
            var target = bb->GetEntityRef(frame, targetKey.Key);
            if (target == default)
                return false;

            var targetT = frame.Get<Transform3D>(target);
            var transform = frame.Get<Transform3D>(entity);
            FP currentDistance = FPVector3.Distance(transform.Position, targetT.Position);

            return currentDistance < distance;
        }
    }
}