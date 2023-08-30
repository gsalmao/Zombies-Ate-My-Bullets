using Quantum;
using UnityEngine;

namespace ZAMB.Entities
{
    public abstract class EntitySetup : MonoBehaviour
    {
        protected EntityRef entityRef;

        public virtual void Init() => entityRef = GetComponent<EntityView>().EntityRef;
    }
}
