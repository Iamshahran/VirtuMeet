using MetaLabs.UI;
using Photon.Pun;
using UnityEngine;

namespace MetaLabs.Controller
{
    public class FppMovement : MonoBehaviour
    {
        private CharacterController _controller;
        [SerializeField] float speed = 8f;
        [SerializeField] float gravity = -9.81f;
        [SerializeField] Transform groundCheck;
        [SerializeField] float groundDistance = 0.4f;
        [SerializeField] LayerMask groundMask;
        [SerializeField] GameObject allMenus;
        [SerializeField] GameObject avatarPage;

        private Vector3 _velocity;
        private bool _isGrounded;
        private bool _isLock = false;
        public PhotonView _photonView;
        [HideInInspector] public static bool isMenuOpen = false;
        [HideInInspector] public static bool isAvatarOpen = false;
        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _photonView = GetComponent<PhotonView>();
        }
        void Start()
        {
            if (!_photonView.IsMine)
            {
            
                Destroy(GetComponentInChildren<Camera>().gameObject);
                Destroy(GetComponentInChildren<MenuManager>().gameObject);
                return;
            }
            _isLock = true;
            isMenuOpen = true;
            isAvatarOpen = false;
        
        }

        // Update is called once per frame
        void Update()
        {
            if (!_photonView.IsMine) return;
            Movement();
            LockUnLock();
        }
        void LockUnLock()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_isLock)
                {
                
                    _isLock = false;
                    return;
                }
                if (!_isLock)
                {
                
                    _isLock = true;
                    return;
                }
            }
        }
        void Movement()
        {
            if (_isLock || isMenuOpen || isAvatarOpen) return;
            _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 move = transform.right * x + transform.forward * z;
            _controller.Move(move * speed * Time.deltaTime);

            _velocity.y += gravity * Time.deltaTime;
            _controller.Move(_velocity * Time.deltaTime);
        }
        public void OpenMenus()
        {
            Debug.Log("menu: "+isMenuOpen+" avatarPage: "+ isAvatarOpen + " Lock: " + _isLock);
            if (!isMenuOpen)
            {
                avatarPage.SetActive(false);
                allMenus.SetActive(true);
                isAvatarOpen = false;
                isMenuOpen = true;
                return;
            }
            if (isMenuOpen)
            {
                allMenus.SetActive(false);
                isMenuOpen = false;
                return;
            }
        }
        public void AvatarPage()
        {
            if (!isAvatarOpen)
            {
                avatarPage.SetActive(true);
                allMenus.SetActive(false);
                isAvatarOpen = true;
                isMenuOpen = false;
                return;
            }
            if (isAvatarOpen)
            {
                avatarPage.SetActive(false);
                isAvatarOpen = false;
                return;
            }
        }
    }
}
