using TMPro;
using UnityEngine;

namespace UI.Handler
{
    public class TextClearHandler : MonoBehaviour
    {
        private TMP_InputField inputField;

        void Awake()
        {
            inputField = GetComponent<TMP_InputField>();
        }

        void OnEnable()
        {
            inputField.text = string.Empty;
        }
    }
}