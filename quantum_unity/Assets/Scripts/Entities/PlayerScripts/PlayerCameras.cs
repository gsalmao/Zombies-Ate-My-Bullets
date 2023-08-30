using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace ZAMB.Entities.PlayerScripts
{
    public class PlayerCameras : MonoBehaviour
    {
        [field: SerializeField] internal Transform LookDirection { get; private set; }

        [SerializeField] private CinemachineFreeLook playerGameplayCam;

        internal void SetupCameras(Transform player)
        {
            playerGameplayCam.Follow = player;
            playerGameplayCam.LookAt = player;
        }
    }
}
