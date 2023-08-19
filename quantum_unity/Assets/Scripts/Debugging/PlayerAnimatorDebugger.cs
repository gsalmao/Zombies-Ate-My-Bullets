using EditorUtilities;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using static ZAMB.Settings.Consts;

namespace ZAMB.Debugging
{
    internal class PlayerAnimatorDebugger : BaseDebugger
    {
        [SerializeField] private Animator playerAnimator;
        [SerializeField] private Animator gunAnimator;
        [SerializeField] private Rig aimRig;
        [SerializeField] private Rig headRig;
        [SerializeField] private float animatorSpeed = 1f;

        private void Update() => playerAnimator.SetFloat(MoveValue, (Mathf.Sin(Time.time * animatorSpeed) + 1) / 2);

        [Button("Aim")]
        private void StartAim()
        {
            playerAnimator.SetBool(Aim, true);
            gunAnimator.SetBool(Aim, true);
            aimRig.weight = 1f;
            headRig.weight = 0f;
        }

        [Button("Stop Aim")]
        private void StopAim()
        {
            playerAnimator.SetBool(Aim, false);
            gunAnimator.SetBool(Aim, false);

            aimRig.weight = 0f;
            headRig.weight = 1f;
        }

        [Button("Shoot")]
        private void PlayShoot()
        {
            playerAnimator.SetTrigger(Shoot);
            gunAnimator.SetTrigger(Shoot);
        }
    }
}
