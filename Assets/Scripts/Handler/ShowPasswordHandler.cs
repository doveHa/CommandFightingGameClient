using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Handler 
{
    public class ShowPasswordHandler : MonoBehaviour
    {
        [SerializeField] private Toggle isShow;
        [SerializeField] private TMP_InputField inputField1;
        [SerializeField] [CanBeNull] private TMP_InputField inputField2;

        public void ShowPassword()
        {
            if (isShow.isOn)
            {
                inputField1.contentType = TMP_InputField.ContentType.Standard;
                if (inputField2 != null)
                {
                    inputField2.contentType = TMP_InputField.ContentType.Standard;
                }
            }
            else
            {
                inputField1.contentType = TMP_InputField.ContentType.Password;
                if (inputField2 != null)
                {
                    inputField2.contentType = TMP_InputField.ContentType.Password;
                }
            }

            inputField1.ForceLabelUpdate();
            if (inputField2 != null)
            {
                inputField2.ForceLabelUpdate();
            }
        }
    }
}