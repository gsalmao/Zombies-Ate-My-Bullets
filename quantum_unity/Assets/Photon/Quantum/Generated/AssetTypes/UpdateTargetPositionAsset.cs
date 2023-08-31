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

[CreateAssetMenu(menuName = "Quantum/AIAction/UpdateTargetPosition", order = Quantum.EditorDefines.AssetMenuPriorityStart + 20)]
public partial class UpdateTargetPositionAsset : AIActionAsset {
  public Quantum.UpdateTargetPosition Settings;

  public override Quantum.AssetObject AssetObject => Settings;
  
  public override void Reset() {
    if (Settings == null) {
      Settings = new Quantum.UpdateTargetPosition();
    }
    base.Reset();
  }
}

public static partial class UpdateTargetPositionAssetExts {
  public static UpdateTargetPositionAsset GetUnityAsset(this UpdateTargetPosition data) {
    return data == null ? null : UnityDB.FindAsset<UpdateTargetPositionAsset>(data);
  }
}
