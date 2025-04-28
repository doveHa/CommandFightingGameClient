using Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Player { get; private set; }
    public GameObject Opponent { get; private set; }
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
        return Player.transform.position.x < Opponent.transform.position.x;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Player = GameObject.Find("Player");
        Instantiate(Resources.Load<GameObject>("Prefabs/Character/" + CharacterManager.Manager.PlayerCharacterName),
            Player.transform);
        
        Opponent = GameObject.Find("Opponent");
        Instantiate(Resources.Load<GameObject>("Prefabs/Character/" + CharacterManager.Manager.OpponentCharacterName),
            Opponent.transform);
        
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}