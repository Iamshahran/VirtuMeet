using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace MetaLabs
{
    public class ShortcutInputFields : MonoBehaviour
    {
        [SerializeField] TMP_InputField[] inputFields;
        [SerializeField] int inputSelected = 0;
        public UnityEvent onEnter;

        private int _totalInputSelected;

        private void Start()
        {
            _totalInputSelected = inputFields.Length - 1;
            inputFields[0].Select();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                inputSelected++;
                if(inputSelected > _totalInputSelected)
                {
                    inputSelected = 0;
                }
                inputFields[inputSelected].Select();

            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (onEnter != null)
                    onEnter.Invoke();
                    
                
            }
        }

    }
}
