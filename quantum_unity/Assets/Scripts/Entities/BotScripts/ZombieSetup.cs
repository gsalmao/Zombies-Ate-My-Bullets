using UnityEngine;

namespace ZAMB.Entities.BotScripts
{
    public class ZombieSetup : EntitySetup
    {
        [SerializeField] private ZombieAnimation zombieAnimation;
        [SerializeField] private ParticleSystem despawnVFX;

        public override void Init()
        {
            base.Init();
            zombieAnimation.Init(entityRef);
        }

        public void OnDespawn() => Instantiate(despawnVFX, transform.position, transform.rotation);
    }
}
