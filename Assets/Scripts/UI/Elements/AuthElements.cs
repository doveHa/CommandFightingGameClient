using System;
using TMPro;
using UnityEngine;

namespace UI.Elements
{
    [Serializable]
    public class AuthElements
    {
        [Header("Login Elements")]
        public TMP_InputField loginId;
        public TMP_InputField loginPw;
        public TMP_InputField userName;
        public GameObject loginGroup;
        
        [Header("Register Elements")]
        public TMP_InputField registerId;
        public TMP_InputField registerPw;
        public TMP_InputField registerPwCheck;
        public GameObject registerGroup;

        public GameObject loginFailed;
        public GameObject registFailedPW;
        public GameObject registSuccess;
    }
}