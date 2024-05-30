using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;

namespace MetaLabs.Interactable
{
    public class XRGrabNetworkInteractable : XRGrabInteractable
    {
        PhotonView photonView;
        // Start is called before the first frame update
        void Start()
        {
            photonView = GetComponent<PhotonView>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        protected override void OnSelectEntered(XRBaseInteractor interactor)
        {
            photonView.RequestOwnership();
            base.OnSelectEntered(interactor);
        }
    }
}
