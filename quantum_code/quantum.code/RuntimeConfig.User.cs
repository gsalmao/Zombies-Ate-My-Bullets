using Photon.Deterministic;
using System;

namespace Quantum {
  partial class RuntimeConfig
    {
        public AssetRef AIBlackboardInitializer;

        partial void SerializeUserData(BitStream stream)
        {
            stream.Serialize(ref AIBlackboardInitializer.Id);
        }
    }
}