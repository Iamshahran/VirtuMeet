using UnityEngine;

namespace MetaLabs.Core
{
    public class EnableDisableObj : MonoBehaviour
    {
    

        public void EnableDisable(GameObject obj)
        {
            if (obj.activeInHierarchy) obj.SetActive(false);
            else obj.SetActive(true);
        }
    }
}
