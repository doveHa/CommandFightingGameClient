using Unity.VisualScripting;
using UnityEngine;

namespace Manager
{
    public class AnimationManager : MonoBehaviour
    {
        public static AnimationManager Manager;

        [SerializeField] private Animator animator;

        void Awake() 
        {
            if(Manager == null)
            {
                Manager = this;
            }
        }
    }
}