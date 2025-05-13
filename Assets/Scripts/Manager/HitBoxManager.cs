using Unity.VisualScripting;
using UnityEngine;

namespace Manager
{
    public class HitBoxManager : MonoBehaviour
    {
        public static HitBoxManager Manager;
        
        public 
        void Awake() 
        {
            if(Manager == null)
            {
                Manager = this;
            }
        }
        
    }
}