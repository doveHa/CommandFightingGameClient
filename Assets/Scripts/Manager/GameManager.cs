using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        public GameObject Player { get; private set; }
        public Vector2 PlayerCenter { get; set; }
        public GameObject Opponent { get; private set; }
        public Vector2 OpponentCenter { get; set; }

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
            Opponent.GetComponentInChildren<Player>().Flip();
        }

        // Update is called once per frame
        void Update()
        {
            IsPlayerLeft = CalculatePlayerIsLeft();

            if (wasPlayerLeft != IsPlayerLeft)
            {
                Player.GetComponentInChildren<Player>().Flip();
                Opponent.GetComponentInChildren<Player>().Flip();
                wasPlayerLeft = IsPlayerLeft;
            }
        }

        private bool CalculatePlayerIsLeft()
        {
            return Player.transform.GetChild(0).position.x < Opponent.transform.GetChild(0).position.x;
        }

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Player = GameObject.Find("Player");
            Instantiate(
                Resources.Load<GameObject>("Prefabs/Character/" + CharacterManager.Manager.PlayerCharacterName + "/" +
                                           CharacterManager.Manager.PlayerCharacterName),
                Player.transform).tag = "Player";

            Opponent = GameObject.Find("Opponent");
            Instantiate(
                Resources.Load<GameObject>("Prefabs/Character/" + CharacterManager.Manager.OpponentCharacterName + "/" +
                                           CharacterManager.Manager.OpponentCharacterName),
                Opponent.transform).tag = "Opponent";
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}