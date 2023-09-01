namespace Quantum
{
    public class BombSystem : SystemSignalsOnly, ISignalOnTriggerEnter3D
    {
        public void OnTriggerEnter3D(Frame f, TriggerInfo3D info)
        {
            if(f.Has<Bomb>(info.Entity))
            {
                f.Events.ExplodeBomb(info.Entity);
                if(f.Has<Health>(info.Other))
                    f.Signals.OnCharacterDamage(info.Other, info.Entity, 3);

                f.Destroy(info.Entity);
            }
        }

    }
}