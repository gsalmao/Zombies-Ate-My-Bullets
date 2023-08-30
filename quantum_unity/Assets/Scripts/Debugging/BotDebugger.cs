using UnityEngine;
using Quantum;

namespace ZAMB.Debugging
{
    public class BotDebugger : MonoBehaviour
    {
        public void StartDebugging()
        {
            var entity = GetComponent<EntityView>().EntityRef;
            BotSDKDebuggerSystem.AddToDebugger(entity);
        }
    }
}
