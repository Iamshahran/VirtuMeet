using UnityEngine;

namespace MetaLabs.Interactable
{
    public class AutomaticLight : MonoBehaviour
    {
        [SerializeField] GameObject LightGroup;

        private void Start()
        {
            LightGroup.SetActive(false);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.name == "IndexFingerColl")
            {
                if (!LightGroup.activeSelf)
                {
                    LightGroup.SetActive(true);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.name == "IndexFingerColl")
            {
                if (LightGroup.activeSelf)
                {
                    LightGroup.SetActive(false);
                }
            }
        }
    }
}