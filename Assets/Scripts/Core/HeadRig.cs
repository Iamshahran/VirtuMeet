using UnityEngine;

namespace MetaLabs.Core
{
    public class HeadRig : MonoBehaviour
    {
        public Transform headContraint;
        public Vector3 headBodyOffset;
        // Start is called before the first frame update
        void Start()
        {
            headBodyOffset = transform.position - headContraint.position;
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = headContraint.position + headBodyOffset;
            transform.forward = Vector3.ProjectOnPlane(headContraint.up, Vector3.up).normalized;
        }
    }
}
