using UnityEngine;

namespace MetaLabs.Interactable
{
    public class OuterLiftGate : MonoBehaviour
    {
        [SerializeField] Animator animator;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        public void OpenGate()
        {
            animator.SetBool("CloseOutGate", false);

            animator.SetBool("OpenOutGate", true);
        }
        public void CloseGate()
        {
            animator.SetBool("OpenOutGate", false);

            animator.SetBool("CloseOutGate", true);
        }
        public void EndOfAnimation()
        {
            animator.SetBool("OpenOutGate", false);
            animator.SetBool("CloseOutGate", false);
        }
    }
}
