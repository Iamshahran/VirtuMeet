using System;
using System.Collections;
using UnityEngine;

namespace MetaLabs.Core
{
    public class OnSceneLoad : MonoBehaviour
    {
        [SerializeField] GameObject[] activeVrObjects;
        [SerializeField] GameObject[] activeDesktopObjects;
        // Start is called before the first frame update
        private void Start()
        {
            StartCoroutine(ActivateObjects());
            AudioManager.Instance.Play("theme");


        }
        IEnumerator ActivateObjects()
        {
            yield return new WaitForSeconds(1);
            if (ProductConfiguration.Instance.isDeviceVR)
            {
                foreach (var obj in activeVrObjects)
                {
                    if(obj.name == "XR Origin")
                    {
                        yield return new WaitForSeconds(1);
                        obj.SetActive(true);
                        continue;
                    }
                    if (obj)
                    {
                        obj.SetActive(true);
                    }
                }
            
            }
            if (!ProductConfiguration.Instance.isDeviceVR)
            {
                foreach (var obj in activeDesktopObjects)
                {
                    if (obj) obj.SetActive(true);
                }
            
            }

        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
