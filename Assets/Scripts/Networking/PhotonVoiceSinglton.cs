using UnityEngine;

namespace MetaLabs.Networking
{
    public class PhotonVoiceSinglton : MonoBehaviour
    {
        public static PhotonVoiceSinglton Instance;
        // Start is called before the first frame update
        void Start()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            Instance = this;

        }


    }
}
