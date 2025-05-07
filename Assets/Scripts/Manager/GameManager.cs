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
        AddSkillComponent(Player,CharacterManager.Manager.PlayerCharacterName);
        
        Opponent = GameObject.Find("Opponent");
        Instantiate(Resources.Load<GameObject>("Prefabs/Character/" + CharacterManager.Manager.OpponentCharacterName),
            Opponent.transform);
        AddSkillComponent(Opponent,CharacterManager.Manager.OpponentCharacterName);
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
                player.AddComponent<Fly>().SetCoff();
                player.AddComponent<Hasegi>().SetCoff();
                player.AddComponent<Scratch>().SetCoff();
                player.AddComponent<UpperWing>().SetCoff();
                break;
            case "Kagetsu":
                player.AddComponent<IttoRyotan>().SetCoff();
                player.AddComponent<NageKunai>().SetCoff();
                player.AddComponent<Nageru>().SetCoff();
                player.AddComponent<Sangiri>().SetCoff();
                break;
            case "Vargon":
                player.AddComponent<Curl>().SetCoff();
                player.AddComponent<Grab>().SetCoff();
                player.AddComponent<Rush>().SetCoff();
                player.AddComponent<Slam>().SetCoff();
                break;
        }
    }
}