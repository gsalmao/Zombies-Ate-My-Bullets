using UnityEngine;

namespace ZAMB.Debugging
{
    internal abstract class BaseDebugger : MonoBehaviour
    {
        protected virtual void Awake() => Debug.LogWarning($"Debug class in {gameObject.name}. Remove for build.");
    }
}
