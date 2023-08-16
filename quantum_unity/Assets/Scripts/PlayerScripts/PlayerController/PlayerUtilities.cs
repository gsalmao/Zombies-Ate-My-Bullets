using UnityEngine;

namespace ZAMB.PlayerScripts.PlayerController
{
    public static class PlayerUtilities
    {
        /// <summary>
        /// Convert Input axis to world direction for player input.
        /// </summary>
        public static Vector3 InputToDir(this Vector2 input) => new Vector3(input.x, 0f, input.y);
        
    }
}
