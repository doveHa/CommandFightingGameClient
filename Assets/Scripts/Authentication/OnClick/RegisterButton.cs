using System.Threading.Tasks;
using Core.OnClick;
using TMPro;
using UI.Elements;
using UnityEngine;

namespace Authentication.OnClick
{
    public class RegisterButton : OnClickBase
    {
        [SerializeField] private AuthElements authElements;

        protected override void OnClick()
        {
            if (CheckPasswordSame())
            {
                Authentication.regist(authElements.registerId.text, authElements.registerPw.text,
                    authElements.userName.text);

                //registFailedPW.SetActive(false);
                //registSuccess.SetActive(true);
                //registSuccess.SetActive(false);

                Task.Delay(1000).Wait();

                authElements.registerGroup.SetActive(false);
                authElements.loginGroup.SetActive(true);
            }
            else
            {
                //registFailedPW.SetActive(true);
            }
        }

        private bool CheckPasswordSame()
        {
            return authElements.registerPw.text.Equals(authElements.registerPwCheck.text);
        }
    }
}