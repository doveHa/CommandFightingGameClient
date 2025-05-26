using UnityEngine;

namespace Manager
{
    public class VarManager : MonoBehaviour
    {
        public static VarManager Manager;

        public Player Player { get; set; }
        public Player Opponent { get; set; }

        public GameObject PlayerGameObject { get; set; }
        public GameObject OpponentGameObject { get; set; }
        
        public string PlayerCharacterName { get; set; }
        public string OpponentCharacterName { get; set; }
        
        private void Awake()
        {
            if (Manager == null)
            {
                Manager = this;
            }
        }
        
    }
}