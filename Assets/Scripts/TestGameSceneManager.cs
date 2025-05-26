using Characters.AnimationHandler;
using Manager;
using UnityEngine;

public class TestGameSceneManager : MonoBehaviour
{
    public NaktisAnimationHandler nakHandler;
    void Start()
    {
        CharacterManager.Manager.PlayerCharacterName = "Naktis";
        CharacterManager.Manager.OpponentCharacterName = "Kagetus";

        SteamNetworkManager.Manager.RemoteSteamId = SteamNetworkManager.Manager.PlayerSteamId.Value;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            nakHandler.StartUpperWingAnimation();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            nakHandler.StartFlyAnimation();
        }
    }
}