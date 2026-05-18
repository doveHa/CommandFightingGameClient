using UnityEngine;
using UnityEngine.UI;

namespace Core.OnClick
{
    public abstract class OnClickBase : MonoBehaviour
    {
        protected void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        protected abstract void OnClick();
    }
}