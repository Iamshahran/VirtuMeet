using UnityEngine;

namespace MetaLabs.Core
{
    public class PlatformManager : MonoBehaviour
    {
        PlatformManager Instance;
        private void Awake()
        {
            Instance = this;
        }
        public bool isWeb = false;
        public bool isDesktop = false;
        public bool isXR = true;

    }
}
