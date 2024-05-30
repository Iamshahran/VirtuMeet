using UnityEngine;

namespace MetaLabs.Interactable
{
    public class LiftManager : MonoBehaviour
    {
        [SerializeField] Animator LiftAnimator;
        [SerializeField] GameObject XRManager;

        bool _bottom = true;
        bool _inMovement = false;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.name);
            if (other.name == "XR Origin")
            {
                other.transform.SetParent(gameObject.transform);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.name == "XR Origin")
            {
                other.transform.SetParent(XRManager.transform.parent);
            }
        }
   
        public void LiftBottomToTop()
        {
            if (!_inMovement)
            {
                //animator.SetTrigger("LiftBTTrigger");
                LiftAnimator.SetBool("LiftBT", true);
            
            }
        }
        public void LiftTopToBottom()
        {
            if (!_inMovement)
            {
                LiftAnimator.SetBool("LiftTB", true);
            
            }
        
        }
    
        public void OnAnimationStartLift()
        {
            //if (_bottom)
            //{
            //    _bottom = false;
            //    _inMovement = false;
            //}
        }
        public void OnAnimationEndLift()
        {
            LiftAnimator.SetBool("LiftBT", false);
            LiftAnimator.SetBool("LiftTB", false);

            //if (!_bottom)
            //{
            //    _bottom = true;
            //    _inMovement = false;
            //}
        }
    }
}
