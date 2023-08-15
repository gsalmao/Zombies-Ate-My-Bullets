using EditorUtilities;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace ZAMB.Debugging
{
    internal class PlayerAnimatorDebugger : BaseDebugger
    {
        [SerializeField] private Animator playerAnimator;
        //[SerializeField] private Animator gunAnimator;
        [SerializeField] private Rig rifleRigging;
        [SerializeField] private float animatorSpeed = 1f;

        private const string moveValue = "MoveValue";
        private const string toStandard = "ToStandard";
        private const string toRifle = "ToRifle";

        private const string rest = "Rest";
        private const string aim = "Aim";
        private const string shoot = "Shoot";

        private void Update() => playerAnimator.SetFloat(moveValue, (Mathf.Sin(Time.time * animatorSpeed) + 1) / 2);

        [Button("Get Rifle")]
        private void GetRifle()
        {
            //gunAnimator?.gameObject.SetActive(true);
            playerAnimator.SetTrigger(toRifle);
            //gunAnimator?.Play(rest);
            rifleRigging.weight = 1f;
        }

        [Button("Lose guns")]
        private void LoseGuns()
        {
            //gunAnimator?.gameObject.SetActive(false);
            playerAnimator.SetTrigger(toStandard);
            rifleRigging.weight = 0f;
        }

        [Button("Aim")]
        private void Aim()
        {
            playerAnimator.SetBool(aim, true);
            //gunAnimator?.Play(aim);
        }

        [Button("Shoot")]
        private void Shoot()
        {
            playerAnimator.SetTrigger(shoot);
            //gunAnimator?.Play(shoot);
        }
    }
}
