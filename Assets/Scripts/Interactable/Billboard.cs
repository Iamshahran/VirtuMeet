using UnityEngine;

namespace MetaLabs.Interactable
{
    public class Billboard : MonoBehaviour
    {
        private Transform _mainCameraTransform;

        private void Start()
        {
            _mainCameraTransform = Camera.main.transform;
        }

        private void LateUpdate()
        {
            transform.LookAt(transform.position + _mainCameraTransform.rotation * Vector3.forward,
                _mainCameraTransform.rotation * Vector3.up);
        }
    }
}