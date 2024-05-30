using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace MetaLabs.Networking
{
    public class PlayerListItem : MonoBehaviourPunCallbacks
    {
        [SerializeField] TMP_Text name;
        [SerializeField] TMP_Text email;

        Player player;
        public void SetUp(Player _player)
        {
            player = _player;
            name.text = _player.NickName;
            email.text = (string)_player.CustomProperties["email"];
        }
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            if (player == otherPlayer)
                Destroy(gameObject);
        }
        public override void OnLeftRoom()
        {
            Destroy(gameObject);
        }
    }
}
