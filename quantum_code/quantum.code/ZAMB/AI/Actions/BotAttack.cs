using Photon.Deterministic;
using System;

namespace Quantum
{
    [Serializable]
    public partial class BotAttack : AIAction
    {
        public FPVector3 hitboxExtents;
        public FPVector3 hitboxOffset;

        public override void Update(Frame frame, EntityRef entity, ref AIContext aiContext)
        {
            var transform = frame.Get<Transform3D>(entity);

#if DEBUG
            Transform3D hitbox;
            hitbox.Position =
                transform.Position + transform.Forward * hitboxOffset.Z +
                transform.Right * hitboxOffset.X +
                transform.Up * hitboxOffset.Y;

            Draw.Box(hitbox.Position, hitboxExtents, transform.Rotation, ColorRGBA.Red);
#endif

            var hits = frame.Physics3D.OverlapShape(transform, Shape3D.CreateBox(hitboxExtents, hitboxOffset));
            for (int i = 0; i < hits.Count; i++)
            {
                //TODO: Pegar health component dos hits e aplicar dano com um signal
            }

            frame.Events.BotAttack(entity);
        }
    }
}