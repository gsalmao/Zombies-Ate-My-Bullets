namespace Quantum.ZAMB.Systems
{
    public unsafe class AISystem : SystemMainThread, ISignalOnComponentAdded<HFSMAgent>
    {
        public void OnAdded(Frame f, EntityRef entity, HFSMAgent* component)
        {
            HFSMRoot hfsmRoot = f.FindAsset<HFSMRoot>(component->Data.Root.Id);
            HFSMManager.Init(f, entity, hfsmRoot);
        }

        public override void Update(Frame f)
        {
            var allAgents = f.Filter<HFSMAgent>();
            while(allAgents.NextUnsafe(out var entity, out var agent))
            {
                HFSMManager.Update(f, f.DeltaTime, entity);
            }
        }
    }
}