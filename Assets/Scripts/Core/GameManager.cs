using UnityEngine;

namespace MetaLabs
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public bool isPlayerUnmute = false;
        private void Awake()
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
