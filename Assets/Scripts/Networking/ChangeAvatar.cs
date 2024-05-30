using Photon.Pun;
using Photon.Voice.PUN;
using UnityEngine;
using UnityEngine.SceneManagement;
using Avatar = MetaLabs.Models.Avatar;

namespace MetaLabs.Networking
{
    public class ChangeAvatar : MonoBehaviourPun
    {
        [SerializeField] private Avatar[] avatarList;
        [SerializeField] private PhotonView photonView;
        
    
        private void Awake()
        {
            if (SceneManager.GetActiveScene().name == "EnteryRoom")
            {
                // We don't want this script in EnteryRoom
                this.enabled = false;
            }
            if (GetComponent<PhotonVoiceView>())
            {
                PhotonVoiceView voiceView = GetComponent<PhotonVoiceView>();
                voiceView.enabled = false;
                voiceView.enabled = true;
            }

        }
        private void LateUpdate()
        {
            if (!photonView.IsMine) return;
            //GetAvatar(PlayerPrefs.GetInt("avatar"));
            int number = PlayerPrefs.GetInt(("avatar"));
            photonView.RPC("GetAvatar", RpcTarget.AllBuffered, PlayerPrefs.GetInt("avatar"));
        }
        [PunRPC]
        protected virtual void GetAvatar(int number)
        {
        
            for (int i = 0; i < avatarList.Length; i++)
            {
                if (number == avatarList[i].number)
                {
                    avatarList[i].gameObject.SetActive(true);
                }
                else
                    avatarList[i].gameObject.SetActive(false);

            }
        }
        
    }
}
