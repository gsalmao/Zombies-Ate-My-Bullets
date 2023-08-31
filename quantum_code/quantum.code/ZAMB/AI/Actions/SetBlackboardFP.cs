using Photon.Deterministic;
using System;

namespace Quantum
{
	[Serializable]
	[AssetObjectConfig(GenerateLinkingScripts = true, GenerateAssetCreateMenu = false, GenerateAssetResetMethod = false)]
	public unsafe partial class SetBlackboardFP : AIAction
	{
		public AIBlackboardValueKey Key;
		public FP Value;

		public override unsafe void Update(Frame frame, EntityRef entity, ref AIContext aiContext)
		{
			var bb = frame.Unsafe.GetPointer<AIBlackboardComponent>(entity);
			bb->Set(frame, Key.Key, Value);
		}
	}
}