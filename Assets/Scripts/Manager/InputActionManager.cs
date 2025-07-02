using UnityEngine;

namespace Manager
{
    public class InputActionManager : MonoBehaviour
    {
        public static InputActionManager Manager { get; private set; }
        public ActionsInput Inputs { get; private set; }

        void Awake()
        {
            if (!GameObject.Find("Manager").TryGetComponent<InputActionManager>(out InputActionManager manager) &&
                Manager == null)
            {
                GameObject.Find("Manager").AddComponent<InputActionManager>();
                Manager = GameObject.Find("Manager").GetComponent<InputActionManager>();
                Destroy(gameObject);
            }

            Inputs = new ActionsInput();
            Inputs.Enable();
        }
    }
}