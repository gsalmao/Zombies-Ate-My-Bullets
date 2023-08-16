using UnityEngine;
using UnityEngine.Animations.Rigging;
using static UnityEngine.Animations.Rigging.MultiAimConstraintData;

namespace ZAMB.RiggingUtilities
{
    /// <summary>
    /// Naturally limits how far the MultiAimConstraint will move, by setting the weight value. This prevents constraint position glitches.
    /// </summary>
    public class AimConstraintLimiter : MonoBehaviour
    {
        [SerializeField] private MultiAimConstraint aim;
        [SerializeField] private Transform body;
        [SerializeField] private float maxAngle;
        [SerializeField] private float changeWeightSpeed;

        private Transform _constrainedObject;
        private Transform _sourceObject;
        private Vector3 _aimAxis;
        private float _angleBetween;
        private float weightMod;

        private void Awake()
        {
            _aimAxis = GetAxisAsVector3(aim.data.aimAxis);
            _constrainedObject = aim.data.constrainedObject;
            _sourceObject = aim.data.sourceObjects[0].transform;
        }

        private void FixedUpdate()
        {
            _aimAxis = body.rotation * Vector3.forward;
            UpdateWeight();
        }

        private void UpdateWeight()
        {
            _angleBetween = Vector3.Angle(_aimAxis, _sourceObject.position - _constrainedObject.position);

            if (_angleBetween < maxAngle)
                weightMod = 1f;
            else
                weightMod = -1f;

            aim.weight += weightMod * changeWeightSpeed * Time.deltaTime;
            aim.weight = Mathf.Clamp(aim.weight, 0f, 1f);
        }

        private Vector3 GetAxisAsVector3(Axis axis)
        {
            return axis switch
            {
                Axis.X => Vector3.right,
                Axis.X_NEG => Vector3.left,
                Axis.Y => Vector3.up,
                Axis.Y_NEG => Vector3.down,
                Axis.Z => Vector3.forward,
                Axis.Z_NEG => Vector3.back,
                _ => Vector3.zero
            };
        }
    }
}