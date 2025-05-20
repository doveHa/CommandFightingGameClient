using DataTable;
using Manager;
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

        public bool IsPlayerLeft { get; private set; }
        public static GameManager Manager { get; private set; }

        void Awake()
        {
            if (Manager == null)
            {
                Manager = this;
            }
        }

        // Update is called once per frame
        void Update()
        {
            IsPlayerLeft = CalculatePlayerIsLeft();
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
            //AddSkillComponent(Player, CharacterManager.Manager.PlayerCharacterName);

            Opponent = GameObject.Find("Opponent");
            Instantiate(
                Resources.Load<GameObject>("Prefabs/Character/" + CharacterManager.Manager.OpponentCharacterName + "/" +
                                           CharacterManager.Manager.OpponentCharacterName),
                Opponent.transform).tag = "Opponent";
            //AddSkillComponent(Opponent, CharacterManager.Manager.OpponentCharacterName);
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void AddSkillComponent(GameObject player, string characterName)
        {
            switch (characterName)
            {
                case "Naktis":
                    player.transform.GetChild(0).GetChild(1).gameObject.AddComponent<Fly>().SetCoff();
                    player.transform.GetChild(0).GetChild(1).gameObject.AddComponent<Hasegi>().SetCoff();
                    player.transform.GetChild(0).GetChild(1).gameObject.AddComponent<Scratch>().SetCoff();
                    player.transform.GetChild(0).GetChild(1).gameObject.AddComponent<UpperWing>().SetCoff();
                    break;
                case "Kagetsu":
                    player.transform.GetChild(0).GetChild(1).gameObject.AddComponent<IttoRyotan>().SetCoff();
                    player.transform.GetChild(0).GetChild(1).gameObject.AddComponent<NageKunai>().SetCoff();
                    player.transform.GetChild(0).GetChild(1).gameObject.AddComponent<Nageru>().SetCoff();
                    player.transform.GetChild(0).GetChild(1).gameObject.AddComponent<Sangiri>().SetCoff();
                    break;
                case "Vargon":
                    player.transform.GetChild(0).GetChild(1).gameObject.AddComponent<Curl>().SetCoff();
                    player.transform.GetChild(0).GetChild(1).gameObject.AddComponent<Grab>().SetCoff();
                    player.transform.GetChild(0).GetChild(1).gameObject.AddComponent<Rush>().SetCoff();
                    player.transform.GetChild(0).GetChild(1).gameObject.AddComponent<Slam>().SetCoff();
                    break;
            }
        }
    }
}