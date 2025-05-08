using Manager;
using UnityEngine;

public class TestGameSceneManager : MonoBehaviour
{
    void Start()
    {
        
        CharacterManager.Manager.PlayerCharacterName = "Naktis";
        CharacterManager.Manager.OpponentCharacterName = "Kagetus";
        
        SteamNetworkManager.Manager.RemoteSteamIdString = SteamNetworkManager.Manager.LocalSteamIdString;
    }
}


