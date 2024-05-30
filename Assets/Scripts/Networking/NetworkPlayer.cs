using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.XR;

namespace MetaLabs.Networking
{
    public class NetworkPlayer : MonoBehaviour
    {
        public Transform headAvatar;
        public Transform leftHandModel;
        public Transform rightHandModel;
        
        private PhotonView photonView;
        private InputDevice headDevice;
        private InputDevice leftHandDevice;
        private InputDevice rightHandDevice;

        private GameObject xrOrigin;
        void Start()
        {
            photonView = GetComponent<PhotonView>();
            //StartCoroutine(GetInputDevice(3f));
            xrOrigin = GameObject.Find("XR Origin");
            if (photonView.IsMine)
            {
                foreach (var item in GetComponentsInChildren<Renderer>())
                {
                    item.enabled = false;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (photonView.IsMine)
            {
            
                MapPosition(headAvatar, XRNode.Head);
                MapPosition(leftHandModel, XRNode.LeftHand);
                MapPosition(rightHandModel, XRNode.RightHand);

                gameObject.transform.SetPositionAndRotation(xrOrigin.transform.position, xrOrigin.transform.rotation);

            }
        }
        void MapPosition(Transform target, XRNode node)
        {
            var gameController = new List<InputDevice>();

            InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
            InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation);
            target.SetPositionAndRotation(transform.TransformPoint(position), transform.rotation * rotation);
        }


        IEnumerator GetInputDevice(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            AssignDevice(InputDeviceCharacteristics.HeadMounted, "head", headDevice);
            AssignDevice(InputDeviceCharacteristics.Left, "hand", leftHandDevice);
            AssignDevice(InputDeviceCharacteristics.Right, "hand", rightHandDevice);

        }
        void AssignDevice(InputDeviceCharacteristics characteristcs, string deviceName, InputDevice device)
        {
            var allDevice = new List<InputDevice>();
            if (deviceName == "hand")
            {
                var desiredCharacteristics = characteristcs | InputDeviceCharacteristics.HeldInHand;
                InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, allDevice);
            }
            if (deviceName == "head")
            {
                var desiredCharacteristics = characteristcs;
                InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, allDevice);
            }


            foreach (var newDevice in allDevice)
            {
                device = newDevice;
            }
        }
    }
}
