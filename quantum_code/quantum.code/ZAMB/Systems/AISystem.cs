namespace Quantum.ZAMB.Systems
{
    public unsafe class AISystem : SystemMainThread, ISignalOnComponentAdded<HFSMAgent>
    {
        public void OnAdded(Frame f, EntityRef entity, HFSMAgent* component)
        {
            HFSMRoot hfsmRoot = f.FindAsset<HFSMRoot>(component->Data.Root.Id);

            var bbComponent = new AIBlackboardComponent();
            var bbInitializer = f.FindAsset<AIBlackboardInitializer>(f.RuntimeConfig.AIBlackboardInitializer.Id);

            AIBlackboardInitializer.InitializeBlackboard(f, &bbComponent, bbInitializer);
            HFSMManager.Init(f, entity, hfsmRoot);
            f.Set(entity, bbComponent);
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