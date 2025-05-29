using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Handler
{
    public class ShowPasswordHandler : MonoBehaviour
    {
        [SerializeField] private GameObject Eye;
        [SerializeField] private TMP_InputField inputField1;
        [SerializeField][CanBeNull] private TMP_InputField inputField2;
        private bool isShow = false;
        public GameObject NoLine;

        public void ShowPassword()
        {

            Debug.Log("EyeButtonClick");
            // 마우스로 클릭 시 isShow를 true로 설정
            isShow = !isShow;

            inputField1.contentType = TMP_InputField.ContentType.Password;
            if (inputField2 != null)
            {
                inputField2.contentType = TMP_InputField.ContentType.Password;
            }
            
            inputField1.ForceLabelUpdate();
            if (inputField2 != null)
            {
                inputField2.ForceLabelUpdate();
            }
            NoLine.SetActive(true);
        }

        private void NotShowPassword()
        {
            Debug.Log("NoLineButtonClick");
            isShow = !isShow;

            inputField1.contentType = TMP_InputField.ContentType.Standard;
            if (inputField2 != null)
            {
                inputField2.contentType = TMP_InputField.ContentType.Standard;
            }

            inputField1.ForceLabelUpdate();
            if (inputField2 != null)
            {
                inputField2.ForceLabelUpdate();
            }
            NoLine.SetActive(false);
        }

        private void Start()
        {
            // Eye 오브젝트에 Button 컴포넌트가 있을 경우 클릭 이벤트 등록
            Button eyeButton = Eye.GetComponent<Button>();
            Button NoLineButton = NoLine.GetComponent<Button>();
            if (eyeButton != null)
            {
                eyeButton.onClick.AddListener(ShowPassword);
            }
            if(NoLineButton != null)
            {
                NoLineButton.onClick.AddListener(NotShowPassword);
            }
        }
    }
}