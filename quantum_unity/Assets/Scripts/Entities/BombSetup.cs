using UnityEngine;

namespace ZAMB.Entities
{
    public class BombSetup : MonoBehaviour
    {
        [SerializeField] private ParticleSystem Explosion;

        public void Explode() => Instantiate(Explosion, transform.position, transform.rotation);
        
    }
}
