using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetaLabs
{
    public class ShowAndHideObjectBtn : MonoBehaviour
    {
        [Header("Second gameobject should SetActive to be true or else leave it empty")]
        [SerializeField] private GameObject hideGameObject;
        [SerializeField] private GameObject hideGameObject2;
        private bool _isHide = true;

        private void Start()
        {
            hideGameObject.SetActive(false);
        }

        public void UnHideBtn()
        {
            if (hideGameObject == null && hideGameObject2 == null) return;
            if(hideGameObject2 == null)
            {
                if (_isHide)
                {
                    hideGameObject.SetActive(true);
                    _isHide = false;
                    return;
                }
                hideGameObject.SetActive(false);
                _isHide = true;
            }
            if(hideGameObject != null && hideGameObject2 != null)
            {
                if (_isHide)
                {
                    hideGameObject.SetActive(true);
                    hideGameObject2.SetActive(false);
                    _isHide = false;
                    return;
                }
                hideGameObject.SetActive(false);
                hideGameObject2.SetActive(true);
                _isHide = true;
            }

            
        }
    }
}
