using Photon.Deterministic;
using Quantum.Profiling;
using static Quantum.PlayerStandardSystem;

namespace Quantum
{
    public unsafe class PlayerStandardSystem : SystemMainThreadFilter<Filter>, ISignalOnPlayerDataSet
    {
        private const float rotationDegrees = 5f;

        public struct Filter
        {
            public EntityRef Entity;
            public PlayerStandard* flag;
            public CharacterController3D* KCC;
            public Transform3D* Transform;
            public PlayerLink* Link;
        }

        public override void Update(Frame f, ref Filter filter)
        {
            PlayerRef player = filter.Link->Player;
            Input* input = f.GetPlayerInput(player);

            if(input->Aim && filter.KCC->Grounded)
            {
                f.Remove<PlayerStandard>(filter.Entity);
                f.Add<PlayerAiming>(filter.Entity);
                f.Events.StartAiming(filter.Link->Player);
            }

            if (input->Jump.WasPressed)
                filter.KCC->Jump(f);

            SetMoveConfigs(f, ref filter, input->Run);
            
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

        private void SetMoveConfigs(Frame f, ref Filter filter, bool runButton)
        {
            var config = f.FindAsset<CharacterController3DConfig>(filter.KCC->Config.Id);

            if (runButton)
            {
                config.Acceleration = 80;
                config.MaxSpeed = 12;
            }
            else
            {
                config.Acceleration = 25;
                config.MaxSpeed = 6;
            }

            filter.KCC->SetConfig(f, config);
        }
    }
}