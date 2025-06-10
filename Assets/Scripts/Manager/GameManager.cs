using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        public RawImage playerImageUI;
        public RawImage opponentImageUI;

        public Vector2 PlayerCenter { get; private set; }
        public Vector2 OpponentCenter { get; private set; }

        private bool wasPlayerLeft;
        public bool IsPlayerLeft { get; private set; }

        public GameObject GameEndUI;

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
            PlayerCenter = playerCenter + VarManager.Manager.PlayerGameObject.transform.position;
            VarManager.Manager.PlayerGameObject.transform.GetChild(0).position = PlayerCenter;
        }

        public void SetOpponentCenter(Vector3 opponentCenter)
        {
            OpponentCenter = opponentCenter + VarManager.Manager.OpponentGameObject.transform.position;
            VarManager.Manager.OpponentGameObject.transform.GetChild(0).position = OpponentCenter;
        }

        public Vector3 ProjectileEndDirection(bool isPlayer)
        {
            if (isPlayer)
            {
                return (OpponentCenter - PlayerCenter).normalized;
            }
            else
            {
                return Vector3.zero;
            }
        }

        public IEnumerator EndGame(bool isPlayerWin)
        {
            InputActionManager.Manager.Inputs.Disable();
            
            if (isPlayerWin)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/UIObj/WinObject"), GameEndUI.transform.position,
                    Quaternion.identity);
                VarManager.Manager.Player.Animator.StartWinAnimation();
                VarManager.Manager.Opponent.Animator.StartLoseAnimation();
            }
            else
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/UIObj/LoseObject"), GameEndUI.transform.position,
                    Quaternion.identity);
                VarManager.Manager.Opponent.Animator.StartWinAnimation();
                VarManager.Manager.Player.Animator.StartLoseAnimation();
            }

            if (Input.GetMouseButtonDown(0))
            {
                SceneLoadManager.Manager.LoadUserMainScene();
                yield break;
            }

            yield return new WaitForSeconds(3);
            SceneLoadManager.Manager.LoadUserMainScene();
            InputActionManager.Manager.Inputs.Enable();

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
            VarManager.Manager.PlayerGameObject = Instantiate(
                Resources.Load<GameObject>("Prefabs/Character/" + VarManager.Manager.PlayerCharacterName + "/" +
                                           VarManager.Manager.PlayerCharacterName),
                GameObject.Find("Player").transform);

            VarManager.Manager.OpponentGameObject = Instantiate(
                Resources.Load<GameObject>("Prefabs/Character/" + VarManager.Manager.OpponentCharacterName + "/" +
                                           VarManager.Manager.OpponentCharacterName),
                GameObject.Find("Opponent").transform);

            VarManager.Manager.PlayerOpponentInitialize();
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}