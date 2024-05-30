using MetaLabs.Models;
using UnityEngine;

namespace MetaLabs.Core
{
    public class MyObjectManager : MonoBehaviour
    {
        [SerializeField] MyObject[] objects;
        public void OpenObject(string name)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                if (name == objects[i].name)
                {
                    objects[i].gameObject.SetActive(true);
                }
                else
                {
                    objects[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
