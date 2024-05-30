using UnityEngine;

namespace MetaLabs.UI
{
    public class OpenMenu : MonoBehaviour
    {
        public GameObject canvas;
        public GameObject camera;
        //private Transform canvasTransform;
        [Range(0, 1)]
        public float smoothFactor = 0.5f;

        // how far to stay away fromt he center

        public float offsetRadius = 0.3f;
        public float distanceToHead = 4;
        private void Start()
        {
            //canvas = GameObject.FindGameObjectWithTag("FirstCanvas");
            //canvasTransform = canvas.GetComponent<Transform>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.name == "IndexFingerColl")
            {


                VibrationManager.Instance.SendVibration(false, 0.5f, 0.2f);
                // make the UI always face towards the camera
                canvas.transform.rotation = camera.transform.rotation;
                //canvas.transform.rotation = Quaternion.Euler(0f, camera.transform.rotation.y, 0f);
            

                var cameraCenter = camera.transform.position + camera.transform.forward * distanceToHead;

                var currentPos = canvas.transform.position;

                // in which direction from the center?
                var direction = currentPos - cameraCenter;

                // target is in the same direction but offsetRadius
                // from the center
                var targetPosition = cameraCenter + direction.normalized * offsetRadius;

                // finally interpolate towards this position
                canvas.transform.position = Vector3.Lerp(currentPos, targetPosition, smoothFactor);

                if (!canvas.activeSelf)
                {
                    //canvas.transform.position =  targetPosition;
                
                    canvas.SetActive(true);
                }
                else
                {
                    canvas.SetActive(false);
                }
            }
        }
        public void HideMenu()
        {
            canvas.SetActive(false);
        }

    
    }
}

