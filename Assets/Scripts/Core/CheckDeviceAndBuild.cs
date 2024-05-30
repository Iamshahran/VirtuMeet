using UnityEngine;

namespace MetaLabs.Core
{
    public class CheckDeviceAndBuild : MonoBehaviour
    {
        [SerializeField] bool isGameObjectForVR;

        private void Start()
        {
            //Checking wheather build is based on VR or Desktop/Web
            //If canvas belong to VR and build is also VR, then it must be active
            if (ProductConfiguration.Instance.isDeviceVR && isGameObjectForVR) gameObject.SetActive(true);
            //If canvas belong to VR and build is Desktop, then it must be inactive
            if (ProductConfiguration.Instance.isDeviceVR && !isGameObjectForVR) gameObject.SetActive(false);
            //If canvas belong to desktop and build is VR, then it must be inactive
            if (!ProductConfiguration.Instance.isDeviceVR && isGameObjectForVR) gameObject.SetActive(false);
            //If canvas belong to desktop and build is also desktop, then it must be active
            if (!ProductConfiguration.Instance.isDeviceVR && !isGameObjectForVR) gameObject.SetActive(true);
        }
    }
}
