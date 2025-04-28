using System.Collections;
using UnityEngine;

namespace Handler
{
    public class SkillCoolHandler : MonoBehaviour
    {
        public int Time { private get; set; }
        public bool IsOn { get; private set; }

        void Awake()
        {
            IsOn = true;
        }
        public void StartCoolCoroutine()
        {
            StartCoroutine(CoolStart());
        }

        private IEnumerator CoolStart()
        {
            IsOn = false;
            yield return new WaitForSeconds(Time); // 쿨타임 대기
            IsOn = true;
        }
    }
}