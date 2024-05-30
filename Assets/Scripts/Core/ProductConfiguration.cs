using UnityEngine;

namespace MetaLabs.Core
{
    public class ProductConfiguration : MonoBehaviour
    {
        public static ProductConfiguration Instance;
        public bool isDeviceVR = true;
    
        [SerializeField] GameObject photonVoiceNetwork;
        //public TextAsset jsonTxt;
        //[System.Serializable]
        //public class Player
        //{
        //    public string name;
        //    public string avatar;
        //    public string nickname;
        //}
        //public Player _player = new Player();
        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            Instance = this;
            if (photonVoiceNetwork) Instantiate(photonVoiceNetwork);
        }
        private void Start()
        {
            PlayerPrefs.SetString("name", "name");
            PlayerPrefs.SetInt("avatar", 1);
            PlayerPrefs.SetString("nickname", "name");
            if(!PlayerPrefs.HasKey("login")) PlayerPrefs.SetString("login", "false");
            //_player = JsonUtility.FromJson<Player>(jsonTxt.text);

        }
        private void Update()
        {
        
        }
    }
}

