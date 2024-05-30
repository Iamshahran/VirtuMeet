using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using MetaLabs.UI;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace MetaLabs.Networking
{
    public class LeaveScene : MonoBehaviourPunCallbacks
    {
        [SerializeField] Slider loadingSlider;
        [SerializeField] TMP_Text loadingText;
        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();

        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            //StartCoroutine(LoadLevelAsync("EnteryRoom"));
            SceneManager.LoadScene("EnteryRoom");
            //Debug.Log("Why I am unable to enter this function");
            //PhotonNetwork.Disconnect();
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
            MenuManager.Instance.OpenMenu("leave");

        }
    }
}
