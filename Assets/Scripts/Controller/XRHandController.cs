using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.XR;

namespace MetaLabs.Controller
{
    public enum HandType
    {
        Left,
        Right
    }
    public class XRHandController : MonoBehaviour
    {
        public HandType handType;
        public InputDeviceCharacteristics controllerCharacteristics;
        private Animator animator;
        private InputDevice inputDevice;
        private List<InputFeatureUsage> inputFeatures;
        private float indexValue;
        private float gripValue;
        private bool thumbRest;
        private bool secondaryBtn;
        public int indexFeature;
        private PhotonView photonView;
        //[SerializeField] private GameObject textPanel;
        private void Start()
        {
            StartCoroutine(GetInputDevice(3f));
            animator = GetComponent<Animator>();
            photonView = GetComponent<PhotonView>();
        }
        private void Update()
        {
            AnimateHand();
        }

        IEnumerator GetInputDevice(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);

            //InputDeviceCharacteristics controllerCharacterstics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller;
            //if(handType == HandType.Left)
            //{
            //    controllerCharacterstics = controllerCharacterstics | InputDeviceCharacteristics.Left;
            //}
            //else
            //{
            //    controllerCharacterstics = controllerCharacterstics | InputDeviceCharacteristics.Right;
            //}
            inputFeatures = new List<InputFeatureUsage>();
            List<InputDevice> inputDevices = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, inputDevices);
            if(inputDevices.Count > 0)
            {
                inputDevice = inputDevices[0];
            }
            inputDevice.TryGetFeatureUsages(inputFeatures);

        }
        private void AnimateHand()
        {

            inputDevice.TryGetFeatureValue(CommonUsages.trigger, out indexValue);
            inputDevice.TryGetFeatureValue(CommonUsages.grip, out gripValue);
            inputDevice.TryGetFeatureValue(CommonUsages.secondaryTouch, out thumbRest);
            inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out secondaryBtn);
            //textPanel.SetActive(secondaryBtn);
            animator.SetFloat("CoolGest", Convert.ToSingle(secondaryBtn));
            animator.SetFloat("Index", indexValue);
            animator.SetFloat("Grip", gripValue);
            animator.SetFloat("ThumbRest", Convert.ToSingle(thumbRest));
        }
    }
}