using UnityEngine;

namespace MetaLabs.Interactable
{
    public class whiteBoardCollider : MonoBehaviour
    {
        [SerializeField] Rigidbody markerRB;
        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "Tip" )//"BlackMarker"||other.name =="BlueMarker"||other.name=="GreenMarker"||other.name=="RedMarker"
            {
                markerRB.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.name == "Tip")
            {
                markerRB.constraints = RigidbodyConstraints.None;
            }
        }
    }
}
