using System;
using System.Collections.Generic;
using System.Linq;
using Characters.AnimationHandler;
using Data.FrameRanges;
using DataTable;
using Manager;
using UnityEngine;
using UnityEngine.Serialization;

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