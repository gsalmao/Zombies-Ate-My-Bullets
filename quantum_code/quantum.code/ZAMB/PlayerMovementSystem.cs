using Photon.Deterministic;
using static Quantum.PlayerMovementSystem;

namespace Quantum
{
    public unsafe class PlayerMovementSystem : SystemMainThreadFilter<Filter>, ISignalOnPlayerDataSet
    {
        public struct Filter
        {
            public EntityRef Entity;
            public CharacterController3D* KCC;
            public Transform3D* Transform;
            public PlayerLink* Link;
        }

        public override void Update(Frame f, ref Filter filter)
        {
            var player = filter.Link->Player;       //filter.Link-> returns index of owner player
            var input = f.GetPlayerInput(player);

            if (input->Jump.WasPressed)
                filter.KCC->Jump(f);
            
            //Normalizing speed to prevent cheaters.
            if (input->Direction.SqrMagnitude > 1)
                input->Direction = input->Direction.Normalized;

            filter.KCC->Move(f, filter.Entity, input->Direction.XOY);

            if (input->Direction != default)
                filter.Transform->Rotation = FPQuaternion.LookRotation(input->Direction.XOY);
        }

        //Called everytime a serialized RuntimePlayer is part of a specific tick input
        public void OnPlayerDataSet(Frame f, PlayerRef player)
        {
            var data = f.GetPlayerData(player);
            var prototype = f.FindAsset<EntityPrototype>(data.CharacterPrototype.Id);
            var e = f.Create(prototype);

            //Setting Instantiated Entity to current player that just joined the room.
            if (f.Unsafe.TryGetPointer<PlayerLink>(e, out var pl))
            {
                pl->Player = player;
            }

            if (f.Unsafe.TryGetPointer<Transform3D>(e, out var t))
            {
                t->Position.X = 0 + player;
            }
        }
    }
}