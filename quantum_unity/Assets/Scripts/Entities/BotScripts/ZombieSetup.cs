using UnityEngine;

namespace ZAMB.Entities.BotScripts
{
    public class ZombieSetup : EntitySetup
    {
        [SerializeField] private ZombieAnimation zombieAnimation;

        public override void Init()
        {
            base.Init();
            zombieAnimation.Init(entityRef);
        }
    }
}
