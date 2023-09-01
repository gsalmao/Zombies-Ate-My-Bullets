using System;

namespace Quantum
{
    [Serializable]
    public partial class CheckDeathDecision : HFSMDecision
    {
        public override unsafe bool Decide(Frame frame, EntityRef entity, ref AIContext aiContext)
        {
            var health = frame.Unsafe.GetPointer<Health>(entity);
            return health->IsDead;
        }
    }
}