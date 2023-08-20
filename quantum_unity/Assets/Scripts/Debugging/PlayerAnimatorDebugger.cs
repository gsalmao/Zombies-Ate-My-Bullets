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
        [SerializeField] private float animatorSpeed = 1f;

        private void Update() => playerAnimator.SetFloat(MoveValue, (Mathf.Sin(Time.time * animatorSpeed) + 1) / 2);

        [Button("Aim")]
        private void StartAim()
        {
            playerAnimator.SetBool(Aim, true);
            gunAnimator.SetBool(Aim, true);
        }

        [Button("Stop Aim")]
        private void StopAim()
        {
            playerAnimator.SetBool(Aim, false);
            gunAnimator.SetBool(Aim, false);
        }

        [Button("Shoot")]
        private void PlayShoot()
        {
            playerAnimator.SetTrigger(Shoot);
            gunAnimator.SetTrigger(Shoot);
        }
    }
}
