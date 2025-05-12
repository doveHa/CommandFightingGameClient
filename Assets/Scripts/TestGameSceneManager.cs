using System;
using System.Collections.Generic;
using System.Linq;
using Data.FrameRanges;
using DataTable;
using Manager;
using UnityEngine;
using UnityEngine.Serialization;

public class TestGameSceneManager : MonoBehaviour
{
    void Start()
    {
        CharacterManager.Manager.PlayerCharacterName = "Naktis";
        CharacterManager.Manager.OpponentCharacterName = "Kagetus";

        SteamNetworkManager.Manager.RemoteSteamId = SteamNetworkManager.Manager.PlayerSteamId.Value;
    }

    void Update()
    {

    }
}