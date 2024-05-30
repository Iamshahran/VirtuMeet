using Photon.Pun;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

namespace MetaLabs.Controller
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] bool inIsolateWorld = false;
        [SerializeField] GameObject Avatar;
        PhotonView PV;
        LocomotionSystem LS;
        InputActionManager IAM;
        private void Awake()
        {
            PV = GetComponent<PhotonView>();
            LS = GetComponent<LocomotionSystem>();
            //IAM = GetComponent<InputActionManager>();
        }
        // Start is called before the first frame update
        void Start()
        {
            WantToDisableXRComponent(false);
            Debug.Log(PV.IsMine);
            if (inIsolateWorld)
            {
                WantToDisableXRComponent(true);
                return;
            }
            //if (inIsolateWorld || PV.IsMine)
            //{
            //    GameObject camera = GameObject.Find("Main Camera");
            //    if(camera != null)
            //    {
            //        Avatar.transform.parent = camera.transform;
            //    }
            //}
            //if (inIsolateWorld)
            //    return;
            //if (!PV.IsMine)
            //{
            //    Destroy(GetComponent<Camera>().gameObject);
            //    //Destroy(GetComponentInChildren<Camera>());
            //    //Destroy(GetComponentInChildren<AudioListener>());
            //    LS.enabled = false;
            //    //IAM.enabled = false;
            //}
            if (PV.IsMine)
            {
                WantToDisableXRComponent(true);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!PV.IsMine)
                return;
        }
        void WantToDisableXRComponent(bool wantToDisable)
        {
            var components = GetComponents(typeof(Behaviour));
            foreach(Behaviour com in components)
            {
                //com.gameObject.SetActive(false);
                //What should I do here
                if(com == GetComponent<PlayerController>())
                {
                    continue;
                }
                else
                {
                    com.enabled = wantToDisable;
                }
            }
        }
    }
}
