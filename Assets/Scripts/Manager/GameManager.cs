using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        public Vector2 PlayerCenter { get; private set; }
        public Vector2 OpponentCenter { get; private set; }

        private bool wasPlayerLeft;
        public bool IsPlayerLeft { get; private set; }

        public static GameManager Manager { get; private set; }

        void Awake()
        {
            if (Manager == null)
            {
                Manager = this;
            }
        }

        void Start()
        {
            wasPlayerLeft = true;
            VarManager.Manager.Opponent.Flip();
        }

        void Update()
        {
            IsPlayerLeft = CalculatePlayerIsLeft();

            if (wasPlayerLeft != IsPlayerLeft)
            {
                VarManager.Manager.Player.Flip();
                VarManager.Manager.Opponent.Flip();
                wasPlayerLeft = IsPlayerLeft;
            }
        }

        public void SetPlayerCenter(Vector3 playerCenter)
        {
            PlayerCenter = playerCenter + VarManager.Manager.PlayerGameObject.transform.GetChild(0).position;
        }

        public void SetOpponentCenter(Vector3 opponentCenter)
        {
            OpponentCenter = opponentCenter + VarManager.Manager.OpponentGameObject.transform.GetChild(0).position;

        }
        public Vector3 ProjectileEndDirection()
        {
            return (OpponentCenter - PlayerCenter).normalized;
        }

        private bool CalculatePlayerIsLeft()
        {
            float playerX = VarManager.Manager.PlayerGameObject.transform.position.x;
            float opponentX = VarManager.Manager.OpponentGameObject.transform.position.x;
            return playerX < opponentX;
        }

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            VarManager.Manager.PlayerGameObject = GameObject.Find("Player");
            VarManager.Manager.OpponentGameObject = GameObject.Find("Opponent");

            Instantiate(
                Resources.Load<GameObject>("Prefabs/Character/" + VarManager.Manager.PlayerCharacterName + "/" +
                                           VarManager.Manager.PlayerCharacterName),
                VarManager.Manager.PlayerGameObject.transform).tag = "Player";
            Instantiate(
                Resources.Load<GameObject>("Prefabs/Character/" + VarManager.Manager.OpponentCharacterName + "/" +
                                           VarManager.Manager.OpponentCharacterName),
                VarManager.Manager.OpponentGameObject.transform).tag = "Opponent";

            VarManager.Manager.PlayerOpponentInitialize();
            ;
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}