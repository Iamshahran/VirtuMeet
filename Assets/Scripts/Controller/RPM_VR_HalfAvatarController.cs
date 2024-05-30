using UnityEngine;

namespace MetaLabs.Controller
{
    [System.Serializable]
    public class MapTransformH
    {
        public Transform vrTarget;
        public Transform IKTarget;
        public Vector3 trackingPositionOffset;
        public Vector3 trackingRotationOffset;

        public void MapVRAvatar()
        {
            IKTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
            IKTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
        }
    }
    public class RPM_VR_HalfAvatarController : MonoBehaviour
    {
        [SerializeField] private MapTransform head;


        [SerializeField] private float turnSmoothness;

        [SerializeField] private Transform IKHead;

        [SerializeField] private Vector3 headBodyOffset;

        void LateUpdate()
        {
            transform.position = IKHead.position + headBodyOffset;
            transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(IKHead.forward, Vector3.up).normalized, Time.deltaTime * turnSmoothness); ;
            head.MapVRAvatar();
        
        }
    }
}