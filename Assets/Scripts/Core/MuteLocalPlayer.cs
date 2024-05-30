using Photon.Pun;
using Photon.Voice.Unity;
using UnityEngine;

namespace MetaLabs
{
    public class MuteLocalPlayer : MonoBehaviour
    {
        [SerializeField] AudioSource audioSource;
        [SerializeField] Speaker speaker;
        [SerializeField] PhotonView photonView;

        
        private void Update()
        {
            if (!photonView.IsMine) return;
            if (GameManager.Instance.isPlayerUnmute)
            {
                speaker.enabled = true;
                audioSource.enabled = true;
            }
            else
            {
                speaker.enabled = false;
                audioSource.enabled = false;
            }
        }
    }
}
