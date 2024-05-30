using MetaLabs.Interface;
using UnityEngine;
using Photon.Pun;

namespace MetaLabs.Controller
{
    public class FppMouseLook : MonoBehaviour, IFppMouseLook
    {
        [SerializeField] private float mouseSensitivity = 80f;
        [SerializeField] private Transform playerBody;
        private float _xRotation = 0f;
        private bool _isMenuOpen;
        private bool _isLock = true;
        [SerializeField] private PhotonView photonView;

        void Update()
        {
            if(photonView != null)
            {
                if (!photonView.IsMine) return;
            }
            _isMenuOpen = FppMovement.isMenuOpen;
            
            MouseLook();
            LockUnLock();
        }
        public void LockUnLock()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_isLock)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = false;
                    _isLock = false;
                    return;
                }
                // if not lock then
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                _isLock = true;
            }
        }

        public void MouseLook()
        {
            if (_isLock || _isMenuOpen) return;
               
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);

        }
    }
}
