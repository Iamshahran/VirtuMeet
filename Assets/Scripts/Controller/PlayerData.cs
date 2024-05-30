using UnityEngine;

namespace MetaLabs.Controller
{
    public class PlayerData : MonoBehaviour
    {
        public TextAsset jsonTxt;
        [System.Serializable]
        public  class Player
        {
            public string name;
            public string avatar;
            public string nickname;
        }
        public Player _player = new Player();
        // Start is called before the first frame update
        void Start()
        {
            _player = JsonUtility.FromJson<Player>(jsonTxt.text);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
