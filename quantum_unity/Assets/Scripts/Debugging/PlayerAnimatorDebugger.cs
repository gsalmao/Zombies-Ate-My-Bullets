using EditorUtilities;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using static ZAMB.Settings.Consts;

namespace ZAMB.Debugging
{
    internal class PlayerAnimatorDebugger : BaseDebugger
    {
        [SerializeField] private Animator[] playerAnimators;
        [SerializeField] private float animatorSpeed = 1f;

        private void Update()
        {
            foreach (Animator animator in playerAnimators)
                animator.SetFloat(MoveValue, (Mathf.Sin(Time.time * animatorSpeed) + 1) / 2);
        }

        [Button("Start Jump")]
        private void StartJump()
        {
            foreach (Animator animator in playerAnimators)
                animator.SetBool(Grounded, false);
        }

        [Button("End Jump")]
        private void EndJump()
        {
            foreach (Animator animator in playerAnimators)
                animator.SetBool(Grounded, true);
        }

        [Button("Idle")]
        private void StartIdle()
        {
            foreach (Animator animator in playerAnimators)
                animator.SetFloat(MoveValue, 0f);
        }

        [Button("Walk")]
        private void StartWalk()
        {
            foreach (Animator animator in playerAnimators)
                animator.SetFloat(MoveValue, .5f);
        }

        [Button("Run")]
        private void StartRun()
        {
            foreach (Animator animator in playerAnimators)
                animator.SetFloat(MoveValue, 1f);
        }

        [Button("Aim")]
        private void StartAim()
        {
            foreach (Animator animator in playerAnimators)
                animator.SetBool(Aim, true);
        }

        [Button("Stop Aim")]
        private void StopAim()
        {
            foreach (Animator animator in playerAnimators)
                animator.SetBool(Aim, false);
        }

        [Button("Shoot")]
        private void PlayShoot()
        {
            foreach (Animator animator in playerAnimators)
                animator.SetTrigger(Shoot);
        }
    }
}
