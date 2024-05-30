using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MetaLabs.Controller
{
    public class PlayerNameTag : MonoBehaviourPun
    {
        private PhotonView _photonView;
        [SerializeField] TextMeshProUGUI nameTxt;
        private void Start()
        {
            if (SceneManager.GetActiveScene().name == "EnteryRoom")
            {
                this.enabled = false;
            }
            _photonView = GetComponent<PhotonView>();
        
        }
        private void Update()
        {
            if (photonView.IsMine) return;
            SetName();
        }
        private void SetName() => nameTxt.text = _photonView.Owner.NickName;
    
    }
}
