using UnityEngine.UI;
using UnityEngine;

namespace MetaLabs
{
    public class MuteButton : MonoBehaviour
    {
        private Sprite muteImage;
        [SerializeField] private Sprite unmuteImage;
        [SerializeField] private Button button;
        private bool isUnmute = false;

        private void Start()
        {
            
            GameManager.Instance.isPlayerUnmute = isUnmute;
            muteImage = button.image.sprite;
            if (!isUnmute)
            {
                button.image.sprite = muteImage;
            }

        }
        public void ButtonClicked()
        {
            if (isUnmute)
            {
                button.image.sprite = muteImage;
                isUnmute = false;
                GameManager.Instance.isPlayerUnmute = isUnmute;

            }
            else
            {
                button.image.sprite = unmuteImage;
                isUnmute = true;
                GameManager.Instance.isPlayerUnmute = isUnmute;

            }
        }
    }
}
