using Manager;
using TMPro;
using UnityEngine;

namespace ButtonMapping
{
    public class HomeSceneButtonMapping : MonoBehaviour
    {
        [SerializeField] private TMP_InputField loginId, loginPw, registerId, registerPw, registerPwCheck;
        [SerializeField] private GameObject homeGroup, loginGroup, registerGroup;

        public void ToLogin()
        {
            homeGroup.SetActive(false);
            loginGroup.SetActive(true);
            Clear();
        }

        public void ToRegister()
        {
            homeGroup.SetActive(false);
            registerGroup.SetActive(true);
            Clear();
        }

        public void Login()
        {
            Authentication.Authentication.login(loginId.text, loginPw.text);
        }

        public void LoginToRegister()
        {
            loginGroup.SetActive(false);
            registerGroup.SetActive(true);
            Clear();
        }

        public void Register()
        {
            if (CheckPasswordSame())
            {
                Authentication.Authentication.regist(registerId.text, registerPw.text);
                RegisterToLogin();
            }
            else
            {
                Debug.Log("Passwords do not match");
            }
        }

        public void RegisterToLogin()
        {
            registerGroup.SetActive(false);
            loginGroup.SetActive(true);
            Clear();
        }

        public void ToHome()
        {
            registerGroup.SetActive(false);
            loginGroup.SetActive(false);
            homeGroup.SetActive(true);
            Clear();
        }

        private bool CheckPasswordSame()
        {
            return registerPw.text.Equals(registerPwCheck.text);
        }

        private void Clear()
        {
            loginId.text = "";
            loginPw.text = "";
            registerId.text = "";
            registerPw.text = "";
            registerPwCheck.text = "";
        }
    }
}