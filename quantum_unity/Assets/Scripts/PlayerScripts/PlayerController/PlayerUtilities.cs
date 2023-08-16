using Cinemachine;
using UnityEngine;

namespace ZAMB.PlayerScripts.PlayerController
{
    public static class PlayerUtilities
    {
        /// <summary>
        /// Convert Input axis to world direction for player input.
        /// </summary>
        public static Vector3 InputToDir(this Vector2 input) => new Vector3(input.x, 0f, input.y);

        /// <summary>
        /// Rotate Player around axis according to input. Since it is used in several states, it was extracted from the states to prevent code duplication.
        /// </summary>
        /// <param name="transform">Player</param>
        /// <param name="camera">Player's Camera</param>
        /// <param name="turnTime">How long it takes to turn</param>
        /// <param name="moveDirection">Direction Player is going to turn</param>
        /// <param name="angle">Current Player angle. Has to be stored outside for further reference.</param>
        /// <param name="turnVelocity">Current velocity of the rotation. Has to be stored outside for further reference.</param>
        public static void GetMoveDirection(Transform transform, CinemachineFreeLook camera, Vector2 input, float turnTime, ref Vector3 moveDirection, ref float angle, ref float turnVelocity)
        {
            Vector3 inputCorrectAxis = input.InputToDir();
            float targetAngle = Mathf.Atan2(inputCorrectAxis.x, inputCorrectAxis.z) * Mathf.Rad2Deg + camera.m_XAxis.Value;
            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);
        }

    }
}
