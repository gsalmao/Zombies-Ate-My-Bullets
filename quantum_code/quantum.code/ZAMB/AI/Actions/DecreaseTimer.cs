using System;

namespace Quantum
{
    [Serializable]
    public unsafe partial class DecreaseTimer : AIAction
    {
        public AIBlackboardValueKey Key;

        public override void Update(Frame f, EntityRef e, ref AIContext aiContext)
        {
            unsafe
            {
                var bb = f.Unsafe.GetPointer<AIBlackboardComponent>(e);
                var timer = bb->GetFP(f, Key.Key);
                timer -= f.DeltaTime;

                bb->Set(f, Key.Key, timer);
            }
        }
    }
}