using UnityEngine;

namespace Manager
{
    public class InputActionManager : MonoBehaviour
    {
        public static InputActionManager Manager { get; private set; }
        public ActionsInput Inputs { get; private set; }

        void Awake()
        {
            if (Manager == null)
            {
                DontDestroyOnLoad(this);
                Manager = this;
            }

            Inputs = new ActionsInput();
        }

        void OnEnable()
        {
            Inputs.Enable();
        }

        void OnDisable()
        {
            Inputs.Disable();
        }

        public void UnLockInput()
        {
            Debug.Log("Unlock");
            Inputs.Enable();
        }
        public void LockInput()
        {
            Debug.Log("Lock");
            Inputs.Disable();
        }
    }
}