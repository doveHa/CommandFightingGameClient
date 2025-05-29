using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace Handler
{
    public class ShowPasswordHandler2 : MonoBehaviour
    {
        [SerializeField] private GameObject toggleObject; // 클릭 대상 오브젝트
        [SerializeField] private TMP_InputField inputField1;
        [SerializeField][CanBeNull] private TMP_InputField inputField2;

        private bool isShow = false; // 내부 상태: 비밀번호 표시 여부

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == toggleObject)
                    {
                        // 상태 토글
                        isShow = !isShow;
                        ShowPassword();
                    }
                }
            }
        }

        private void ShowPassword()
        {
            TMP_InputField.ContentType type = isShow
                ? TMP_InputField.ContentType.Standard
                : TMP_InputField.ContentType.Password;

            inputField1.contentType = type;
            inputField1.ForceLabelUpdate();

            if (inputField2 != null)
            {
                inputField2.contentType = type;
                inputField2.ForceLabelUpdate();
            }
        }
    }
}
