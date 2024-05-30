using System;
using UnityEngine;

namespace MetaLabs.Controller
{
    public class FppController : MonoBehaviour
    {
        
        private CharacterController _charController;

        private void Awake()
        {
            _charController = gameObject.AddComponent<CharacterController>();
            _charController.radius = 0.5f;
            _charController.height = 1.8f;
        }
    }
}