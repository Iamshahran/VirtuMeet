using UnityEngine;
using UnityEngine.Events;

namespace MetaLabs.Interactable
{
    public class ButtonVR : MonoBehaviour
    {
        [SerializeField] private float threshold = .1f;
        [SerializeField] private float deadzone = .025f;
        public UnityEvent onPress, onRelease;


        bool _isPressed;
        Vector3 _startPos;
        ConfigurableJoint _joint;
        AudioSource _audio;

        void Start()
        {
            _startPos = transform.localPosition;
            _joint = GetComponent<ConfigurableJoint>();
            _audio = GetComponent<AudioSource>();
        }

        void Pressed()
        {
            _isPressed = true;
            onPress.Invoke();
            _audio.Play();
            Debug.Log("press");
        }

        void Released()
        {
            _isPressed = false;
            onRelease.Invoke();
            Debug.Log("release");

        }

        void Update()
        {
            if (!_isPressed && GetValue() + threshold >= 1)
                Pressed();
            if (_isPressed && GetValue() - threshold <= 0)
                Released();
        }

        float GetValue()
        {
            var value = Vector3.Distance(_startPos, transform.localPosition) / _joint.linearLimit.limit;
            if (Mathf.Abs(value) < deadzone)
                value = 0;
            return Mathf.Clamp(value, -1f, 1f);
        }
    }
}