using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace MetaLabs
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;
        [SerializeField] GameObject loaderCanvas;
        [SerializeField] Slider progressBar;
        [SerializeField] TMP_Text percentText;
        private float _target;
        float _progress;


        private void Start()
        {
            loaderCanvas.SetActive(false);
            StartCoroutine(LoadAsync("EnteryRoom",15));
        }

        private void Update()
        {
            percentText.text = ((int)_progress * 100).ToString() + "%";

        }
        IEnumerator LoadAsync(string sceneName, int waitSec)
        {
            percentText.text = "0%";
            if(waitSec != null)
            {
                yield return new WaitForSeconds(waitSec);
            }
            var scene = SceneManager.LoadSceneAsync(sceneName);
            loaderCanvas.SetActive(true);

            while (!scene.isDone)
            {
                _progress = Mathf.Clamp01(scene.progress / 0.9f);
                progressBar.value = _progress;
                percentText.text = ((int)_progress * 100).ToString() + "%";
                yield return null;
            }
        }
    }
}
