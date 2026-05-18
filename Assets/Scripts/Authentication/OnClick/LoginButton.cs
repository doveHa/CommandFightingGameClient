using Core.OnClick;
using TMPro;
using UI.Elements;
using UnityEngine;

namespace Authentication.OnClick
{
    public class LoginButton : OnClickBase
    {
        [SerializeField] private AuthElements authElements;
     
        protected override async void OnClick()
        {
            if (!await Authentication.login(authElements.loginId.text, authElements.loginPw.text))
            {
                //loginFailed.SetActive(true);
            }
        }
    }
}