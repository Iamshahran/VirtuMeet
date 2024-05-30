using System.IO;
using MetaLabs.Core;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MetaLabs.Controller
{
    public class PlayerManager : MonoBehaviour
    {
        private GameObject spawnedPlayerPrefab;
        PhotonView PV;
        private void Awake()
        {
            PV = GetComponent<PhotonView>();
        }
        private void Start()
        {
            if (PV.IsMine)
            {
                CreateController();
            }
        }
        void CreateController()
        {
            Debug.Log("Instansiate Player Controller");
            Vector3 PlayerPosition = new Vector3(0, 0, 0);
            if (SceneManager.GetActiveScene().name == "MeetingRoom")
            {
                PlayerPosition = new Vector3(84,10,64);
            }
            else if(SceneManager.GetActiveScene().name == "PresentationRoom")
            {
                PlayerPosition = new Vector3(13,3,1);
            }
        
            // Instansiate Player controller
            if (!ProductConfiguration.Instance.isDeviceVR)
            {
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "FPP Controller"), PlayerPosition, Quaternion.Euler(0,-54,0));
                return;
            }
            if (ProductConfiguration.Instance.isDeviceVR)
            {
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Network Player"), transform.position, transform.rotation);
                return;
            }


        }
    }
}
