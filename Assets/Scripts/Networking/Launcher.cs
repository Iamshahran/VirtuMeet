using System;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

using System.Collections.Generic;
using ExitGames.Client.Photon;
using MetaLabs.Core;
using MetaLabs.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MetaLabs.Networking
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        [SerializeField] TMP_InputField roomNameInputField;
        [SerializeField] TMP_InputField hostNameInputField;
        [SerializeField] TMP_Text errorText;
        [SerializeField] TMP_Text roomNameText;
        [SerializeField] Transform roomListContent;
        [SerializeField] Transform playerListContent;
        [SerializeField] GameObject roomListPrefab;
        [SerializeField] GameObject playerListPrefab;
        [SerializeField] GameObject startButtonPrefab;
        [SerializeField] TMP_Text startButtonTxt;
        [SerializeField] TMP_Text leaveButtonTxt;
        [SerializeField] TMP_Dropdown roomTemplateDdp;
        [SerializeField] Slider loadingSlider; 
        [SerializeField] TMP_Text loadingText;


        public GameObject[] EnableObjectsOnConnect;
        public GameObject[] DisableObjectsOnConnect;
    
        private Dictionary<string, RoomInfo> cachedRoomList;
    

        public static Launcher Instance;
        private void Awake()
        {
            Instance = this;
            cachedRoomList = new Dictionary<string, RoomInfo>();

        }
        void Start()
        {

            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.ConnectUsingSettings();
            }
            if(PhotonNetwork.IsConnected && !PhotonNetwork.InLobby)
            {
                PhotonNetwork.JoinLobby();
            }
        }
        public override void OnConnectedToMaster()
        {
            foreach(var obj in EnableObjectsOnConnect)
            {
                obj.SetActive(true);
            }
            foreach(var obj in DisableObjectsOnConnect)
            {
                obj.SetActive(false);
            }
            if(!PhotonNetwork.InLobby) 
                PhotonNetwork.JoinLobby();
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        public override void OnJoinedLobby()
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString("nickname") != "" || PlayerPrefs.GetString("nickname") != null ? PlayerPrefs.GetString("nickname") : PlayerPrefs.GetString("member_name");


            //ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
            Hashtable hash = new Hashtable();
            hash.Add("email",PlayerPrefs.GetString("email"));
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
            //PhotonNetwork.LocalPlayer.CustomProperties = hash;

        }

        public void CreateRoom()
        {
            if (string.IsNullOrEmpty(roomNameInputField.text) || string.IsNullOrEmpty(hostNameInputField.text))
            {
                return;
            }

            RoomOptions roomOptions = new RoomOptions()
            {
                IsVisible = true, IsOpen = true, MaxPlayers = 10
            };
            roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
            roomOptions.CustomRoomProperties.Add("DateTime",DateTime.Now.ToString("F"));
            roomOptions.CustomRoomProperties.Add("Host",PhotonNetwork.NickName);
            roomOptions.CustomRoomPropertiesForLobby = new string[] { "Host", "DateTime" };
            PhotonNetwork.CreateRoom(roomNameInputField.text, roomOptions);
            MenuManager.Instance.OpenMenu("loading");
            
        }

        public override void OnJoinedRoom()
        {
            MenuManager.Instance.OpenMenu("joinroom");
            roomNameText.text = PhotonNetwork.CurrentRoom.Name;

            Player[] player = PhotonNetwork.PlayerList;
            foreach(Transform child in playerListContent)
            {
                Destroy(child.gameObject);
            }

            for(int i = 0; i < player.Length; i++)
            {
                if (player[i] != null)
                {
                    Instantiate(playerListPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(player[i]);

                }
            }

            if (PhotonNetwork.IsMasterClient)
            {
                startButtonTxt.text = "Start Meeting";
                leaveButtonTxt.text = "End Meeting";
            }
            startButtonPrefab.SetActive(PhotonNetwork.IsMasterClient);
        }
        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            startButtonPrefab.SetActive(PhotonNetwork.IsMasterClient);
            if (PhotonNetwork.IsMasterClient)
            {
                startButtonTxt.text = "Start Meeting";
                leaveButtonTxt.text = "End Meeting";
            }
        }
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            MenuManager.Instance.OpenMenu("error");
            errorText.text = "Room creation failed: " + message;
        }
        public void StartGame()
        {
            string sceneName = "MeetingRoom";
            //PhotonNetwork.LoadLevel(roomTemplateDdp.value + 1);
            if (roomTemplateDdp.value == 0) sceneName = "MeetingRoom";
            if (roomTemplateDdp.value == 1) sceneName = "PresentationRoom";
            StartCoroutine(LoadLevelAsync(sceneName));
            FindObjectOfType<AudioManager>().Play("click");

        }
        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
            MenuManager.Instance.OpenMenu("loading");
            FindObjectOfType<AudioManager>().Play("click");

        }
        public override void OnLeftRoom()
        {
            MenuManager.Instance.OpenMenu("collab");
        }
        public void JoinRoom(RoomInfo info)
        {
            PhotonNetwork.JoinRoom(info.Name);
            MenuManager.Instance.OpenMenu("loading");
        }
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Instantiate(playerListPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
        }
        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            // foreach (Transform trans in roomListContent)
            // {
            //     Destroy(trans.gameObject);
            // }
            // for (int i = 0; i < roomList.Count; i++)
            // {
            //     if (roomList[i].RemovedFromList)
            //         continue;
            //     Instantiate(roomListPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
            // }
            ClearRoomListView();
            UpdateCashedRoomList(roomList);
            // foreach (RoomInfo roomInfo in cachedRoomList.Values)
            // {
            //     //if (roomInfo.RemovedFromList)
            //         //continue;
            //     Instantiate(roomListPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomInfo);
            // }
            UpdateRoomListView();
            //if(cachedRoomList.Count != 0) UpdateRoomListView();
        }

        private void ClearRoomListView()
        {
            foreach (Transform trans in roomListContent)
            {
                Destroy(trans.gameObject);
            }
        }

        private void UpdateCashedRoomList(List<RoomInfo> roomInfo)
        {
            foreach (RoomInfo info in roomInfo)
            {
                // Remove room from cached room list if it got closed, became invisible or was marked as removed
                if (!info.IsOpen || !info.IsVisible || info.RemovedFromList)
                {
                    if (cachedRoomList.ContainsKey(info.Name))
                    {
                        cachedRoomList.Remove(info.Name);
                    }

                    continue;
                }

                // Update cached room info
                if (cachedRoomList.ContainsKey(info.Name))
                {
                    cachedRoomList[info.Name] = info;
                }
                // Add new room info to cache
                else
                {
                    cachedRoomList.Add(info.Name, info);
                }
            }
        }

        private void UpdateRoomListView()
        {
            int sno = 0;
            foreach (RoomInfo roomInfo in cachedRoomList.Values)
            {
                if (roomInfo.RemovedFromList)
                    continue;
                sno++;
                Instantiate(roomListPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomInfo, sno);
            }
        }
        IEnumerator LoadLevelAsync(string sceneName)
        {
            //loadingScreen.SetActive(true);
            MenuManager.Instance.OpenMenu("loadingscreen");
            PhotonNetwork.LoadLevel(sceneName);

            while (PhotonNetwork.LevelLoadingProgress < 1)
            {
                loadingText.text = (int)(PhotonNetwork.LevelLoadingProgress * 100) + "%";
                //loadAmount = async.progress;
                loadingSlider.value = PhotonNetwork.LevelLoadingProgress;
                yield return new WaitForEndOfFrame();
            }
            //loadingScreen.SetActive(false);
            MenuManager.Instance.OpenMenu("roomlist");

        }
    }
}