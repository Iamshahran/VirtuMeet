using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace MetaLabs.Networking
{
    public class RoomListItem : MonoBehaviour
    {
        [SerializeField] TMP_Text sno;
        [SerializeField] TMP_Text roomName;
        [SerializeField] TMP_Text createdBy;
        [SerializeField] TMP_Text dateTime;
        public RoomInfo info;
        public void SetUp(RoomInfo _info, int sNo)
        {
            info = _info;
            string hostName = (string) info.CustomProperties["Host"];
            Debug.Log(hostName);
            Debug.Log(info.CustomProperties["DateTime"]);
            roomName.text = info.Name;
            sno.text = sNo.ToString();
            createdBy.text = (string) info.CustomProperties["Host"];
            dateTime.text = (string)info.CustomProperties["DateTime"];
        }
        public void OnClick()
        {
            Launcher.Instance.JoinRoom(info);
        }
    }
}
