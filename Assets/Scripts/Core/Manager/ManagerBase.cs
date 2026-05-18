using UnityEngine;

namespace Core.Manager
{
    public class ManagerBase<T> : MonoBehaviour where T : ManagerBase<T>
    {
        public static ManagerBase<T> Manager;

        protected virtual void Awake()
        {
            if (Manager == null)
            {
                Manager = (T)this;
            }
        }
    }
}