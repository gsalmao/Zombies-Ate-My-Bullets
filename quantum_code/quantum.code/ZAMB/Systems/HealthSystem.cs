using Photon.Deterministic;

namespace Quantum
{
    public unsafe class HealthSystem : SystemMainThreadFilter<HealthSystem.Filter>, ISignalOnCharacterDamage, ISignalOnComponentAdded<Health>
    {
        private static readonly FP HealPercentage = FP._0_10 + FP._0_03;
        private static readonly FP DelayToStartHealing = 3;
        private static readonly FP DelayToHeal = 1;

        public struct Filter
        {
            public EntityRef Entity;
            public Health* Health;
        }

        public void OnAdded(Frame frame, EntityRef entity, Health* component)
        {
            component->CurrentValue = component->InitialValue;
        }


        public override void Update(Frame frame, ref Filter filter)
        {

        }


        public void OnCharacterDamage(Frame frame, EntityRef targetCharacter, EntityRef sourceCharacter, int damage)
        {
            if (frame.Unsafe.TryGetPointer<Health>(targetCharacter, out var health))
            {
                health->CurrentValue -= damage;

                frame.Events.CharacterDamaged(targetCharacter, damage);

                if (health->CurrentValue <= 0)
                {
                    health->IsDead = true;
                    frame.Signals.OnCharacterDie(targetCharacter, sourceCharacter);
                }
            }
        }
    }
}
