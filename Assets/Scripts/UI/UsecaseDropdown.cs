using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetaLabs
{
    public class UsecaseDropdown : MonoBehaviour
    {
        [SerializeField] GameObject[] Banking;
        [SerializeField] GameObject[] Healthcare;
        [SerializeField] GameObject[] Manufacturing;

        public void HandleInputData(int value)
        {
            if(value == 0)
            {
                foreach(var obj in Banking)
                {
                    obj.SetActive(true);
                }
                foreach (var obj in Healthcare)
                {
                    obj.SetActive(false);
                }
                foreach (var obj in Manufacturing)
                {
                    obj.SetActive(false);
                }
                
            }
            if (value == 1)
            {
                foreach (var obj in Banking)
                {
                    obj.SetActive(false);
                }
                foreach (var obj in Healthcare)
                {
                    obj.SetActive(true);
                }
                foreach (var obj in Manufacturing)
                {
                    obj.SetActive(false);
                }

            }
            if (value == 2)
            {
                foreach (var obj in Banking)
                {
                    obj.SetActive(false);
                }
                foreach (var obj in Healthcare)
                {
                    obj.SetActive(false);
                }
                foreach (var obj in Manufacturing)
                {
                    obj.SetActive(true);
                }

            }
        }
    }
}
