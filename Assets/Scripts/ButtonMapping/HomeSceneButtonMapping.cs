using Manager;
using System;
using TMPro;
using UnityEngine;
using System.Threading.Tasks;

namespace ButtonMapping
{
    public class HomeSceneButtonMapping : MonoBehaviour
    {
        [SerializeField] private TMP_InputField loginId, loginPw, registerId, registerPw, registerPwCheck;
        [SerializeField] private GameObject homeGroup, loginGroup, registerGroup;
        public GameObject loginFailed;
        public GameObject registFailedPW;
        public GameObject registSuccess;

        public void ToLogin()
        {
            homeGroup.SetActive(false);
            loginGroup.SetActive(true);
            loginFailed.SetActive(false);
            Clear();
        }

        public void ToRegister()
        {
            homeGroup.SetActive(false);
            registerGroup.SetActive(true);
            Clear();
        }

        public async void Login()
        {
            string message = await Authentication.Authentication.login(loginId.text, loginPw.text);
            if(message.Equals("{\"message\":\"User not found.\"}")||message.Equals("{\"message\":\"Invalid login password\"}"))
            {
                loginFailed.SetActive(true);
            }
        }

        public void LoginToRegister()
        {
            loginGroup.SetActive(false);
            registerGroup.SetActive(true);
            loginFailed.SetActive(false);
            Clear();
        }

        public async void Register()
        {
            if (CheckPasswordSame())
            {
                Authentication.Authentication.regist(registerId.text, registerPw.text);

                registFailedPW.SetActive(false);
                registSuccess.SetActive(true);

                await Task.Delay(1000); // 1�� ���

                registSuccess.SetActive(false);

                RegisterToLogin();
            }
            else
            {
                registFailedPW.SetActive(true);
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