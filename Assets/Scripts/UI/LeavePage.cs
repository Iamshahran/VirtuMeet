using MetaLabs.Networking;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;
namespace MetaLabs.UI
{
    public class LeavePage : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Transform playerListContent;

        [SerializeField] private GameObject playerListPrefab;
        [SerializeField] private TMP_Text roomNameText;

        void Update()
        {
            //if(PhotonNetwork.InRoom!) return;
            roomNameText.text = PhotonNetwork.CurrentRoom.Name;
            Player[] players = PhotonNetwork.PlayerList;
            foreach (Transform child in playerListContent)
            {
                Destroy(child.gameObject);
            }
            for(int i = 0; i < players.Length; i++)
            {
                if (players[i] != null)
                {
                    Instantiate(playerListPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);

                }
            }
        }
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Instantiate(playerListPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
        }
    }
}
