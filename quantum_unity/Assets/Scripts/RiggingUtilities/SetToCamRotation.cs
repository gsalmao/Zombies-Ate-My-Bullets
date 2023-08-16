using UnityEngine;

namespace ZAMB.RiggingUtilities
{

    public class SetToCamRotation : MonoBehaviour
    {
        private Transform _camera;

        private void Awake() => _camera = Camera.main.transform;
        void Update() => transform.rotation = _camera.rotation;
    }
}