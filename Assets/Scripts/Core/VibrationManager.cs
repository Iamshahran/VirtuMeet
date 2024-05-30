using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using MetaLabs.Core;

namespace MetaLabs
{
    public class VibrationManager : MonoBehaviour
    {
        public static VibrationManager Instance;
        public string leftHandTagName = "Left Hand";
        public string rightHandTagName = "Right Hand";
        public float defaultAmplitude = 0.2f;
        public float defaultDuration = 0.3f;

        private XRBaseController leftController;
        private XRBaseController rightController;
        [Header("Hover Vibration")]
        public float hoverAmplitude;
        public float hoverDuration;
        [Header("Click Vibration")]
        public float clickAmplitude;
        public float clickDuration;

        private void Awake()
        {
            if (!ProductConfiguration.Instance.isDeviceVR) return;
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            Instance = this;
            leftController = GameObject.FindGameObjectWithTag(leftHandTagName).GetComponent<XRBaseController>();
            rightController = GameObject.FindGameObjectWithTag(rightHandTagName).GetComponent<XRBaseController>();
        }
        private void Start()
        {
            
        }
        public void HoverVibration()
        {
            if (!ProductConfiguration.Instance.isDeviceVR) return;

            //leftController.SendHapticImpulse(hoverAmplitude, hoverDuration);
            rightController.SendHapticImpulse(hoverAmplitude, hoverDuration);
        }
        public void ClickVibration()
        {
            if (!ProductConfiguration.Instance.isDeviceVR) return;

            //leftController.SendHapticImpulse(clickAmplitude, clickDuration);
            rightController.SendHapticImpulse(clickAmplitude, clickDuration);
        }
        public void SendVibration()
        {
            if (!ProductConfiguration.Instance.isDeviceVR) return;

            //leftController.SendHapticImpulse(defaultAmplitude, defaultDuration);
            rightController.SendHapticImpulse(defaultAmplitude, defaultDuration);
        }
        public void SendVibration(float amplitude,float duration)
        {
            if (!ProductConfiguration.Instance.isDeviceVR) return;

            //leftController.SendHapticImpulse(amplitude, duration);
            rightController.SendHapticImpulse(amplitude, duration);
        }
        public void SendVibration(bool isLeftController,float amplitude, float duration)
        {
            if (!ProductConfiguration.Instance.isDeviceVR) return;

            if (isLeftController)
            {
                leftController.SendHapticImpulse(amplitude, duration);
            }
            else
            {
                rightController.SendHapticImpulse(amplitude, duration);
            }
            
        }
    }
}
