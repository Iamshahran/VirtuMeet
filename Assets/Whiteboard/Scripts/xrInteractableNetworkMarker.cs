using Photon.Pun;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class xrInteractableNetworkMarker : XRGrabInteractable
{
    Rigidbody rb;
    Transform originalTrans;
    [SerializeField] float markerAngle;
    PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();
        originalTrans = GetComponent<Transform>();
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
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log(args.interactorObject);
        if (args.interactorObject.transform.CompareTag("Left Hand") || args.interactorObject.transform.CompareTag("Right Hand"))
        {
            gameObject.transform.rotation = Quaternion.Euler(markerAngle, 0f, 0f);
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
        base.OnSelectEntered(args);
    }
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        if (args.interactorObject.transform.CompareTag("Left Hand") || args.interactorObject.transform.CompareTag("Right Hand"))
        {
            //gameObject.transform.position = originalTrans.position;
            //gameObject.transform.rotation = originalTrans.rotation;
            //rb.constraints = RigidbodyConstraints.None;
        }
        base.OnSelectExited(args);
    }
    public void ResetPosition()
    {
        this.transform.position = originalTrans.position;
        this.transform.rotation = originalTrans.rotation;
        this.transform.localScale = originalTrans.localScale;
    }
}
