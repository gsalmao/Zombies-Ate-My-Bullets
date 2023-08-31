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

[CreateAssetMenu(menuName = "Quantum/AIAction/DecreaseTimer", order = Quantum.EditorDefines.AssetMenuPriorityStart + 3)]
public partial class DecreaseTimerAsset : AIActionAsset {
  public Quantum.DecreaseTimer Settings;

  public override Quantum.AssetObject AssetObject => Settings;
  
  public override void Reset() {
    if (Settings == null) {
      Settings = new Quantum.DecreaseTimer();
    }
    base.Reset();
  }
}

public static partial class DecreaseTimerAssetExts {
  public static DecreaseTimerAsset GetUnityAsset(this DecreaseTimer data) {
    return data == null ? null : UnityDB.FindAsset<DecreaseTimerAsset>(data);
  }
}
