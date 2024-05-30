using MetaLabs.Interface;
using Photon.Pun;
using TMPro;
using UnityEngine;
using Avatar = MetaLabs.Models.Avatar;

namespace MetaLabs.Networking
{
    public class AvatarSelector : MonoBehaviour, IAvatarSelector
    {
        
        [SerializeField] private Avatar[] avatarList;
        //[SerializeField] private PhotonView photonView;
        [SerializeField] private TMP_InputField avatarName;
        private int _avatarNum;
        private void Start()
        {
            PlayerPrefs.SetString("nickname","NoName");
            GetAvatar(PlayerPrefs.GetInt("avatar"));
            avatarName.text = PlayerPrefs.GetString("nickname").ToString();
           
        }
        
        public void GetAvatar(int number)
        {
            _avatarNum = number;
            for(int i = 0; i < avatarList.Length; i++)
            {
                if (number == avatarList[i].number)
                {
                    avatarList[i].gameObject.SetActive(true);
                    
                }
                else
                    avatarList[i].gameObject.SetActive(false);

            }
        }
        public void SetAvatar()
        {
            PlayerPrefs.SetString("nickname",avatarName.text);
            Debug.Log("nick name is " + PlayerPrefs.GetString("nickname"));
            string name = PlayerPrefs.GetString("nickname");
            PhotonNetwork.NickName = name;
            PlayerPrefs.SetInt("avatar", _avatarNum);
        }


    }
    
}