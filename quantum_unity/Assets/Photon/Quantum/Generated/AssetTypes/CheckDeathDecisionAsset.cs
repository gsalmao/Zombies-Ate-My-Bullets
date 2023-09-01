// <auto-generated>
// This code was auto-generated by a tool, every time
// the tool executes this code will be reset.
//
// If you need to extend the classes generated to add
// fields or methods to them, please create partial  
// declarations in another file.
// </auto-generated>

using Quantum;
using UnityEngine;

[CreateAssetMenu(menuName = "Quantum/HFSMDecision/CheckDeathDecision", order = Quantum.EditorDefines.AssetMenuPriorityStart + 184)]
public partial class CheckDeathDecisionAsset : HFSMDecisionAsset {
  public Quantum.CheckDeathDecision Settings;

  public override Quantum.AssetObject AssetObject => Settings;
  
  public override void Reset() {
    if (Settings == null) {
      Settings = new Quantum.CheckDeathDecision();
    }
    base.Reset();
  }
}

public static partial class CheckDeathDecisionAssetExts {
  public static CheckDeathDecisionAsset GetUnityAsset(this CheckDeathDecision data) {
    return data == null ? null : UnityDB.FindAsset<CheckDeathDecisionAsset>(data);
  }
}
