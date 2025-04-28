using System;
using UnityEngine;
using Handler;
using Manager;
using Server;

namespace ButtonMapping
{
    public class UsermainSceneButtonMapping : MonoBehaviour
    {
        [SerializeField] private GameObject userMainGroup, characterInfoGroup;
        [SerializeField] private CharacterSelectHandler characterSelectHandler;
        [SerializeField] private CommandRecodeHandler commandRecodeHandler;
        [SerializeField] private WebSocketClient webSocketClient;
        
        public void mainToCharacterInfo()
        {
            userMainGroup.SetActive(false);
            characterInfoGroup.SetActive(true);
        }

        public void characterInfoToMain()
        {
            characterInfoGroup.SetActive(false);
            characterSelectHandler.AllDescriptionOff();
            userMainGroup.SetActive(true);
        }

        public void Select()
        {
            if (CharacterSelectHandler.CurrentShowCharacter != null)
            {
                CharacterManager.Manager.PlayerCharacterName = CharacterSelectHandler.CurrentShowCharacter;
            }

            characterInfoToMain();

            CharacterManager.Manager.CharacterOn();
        }

        public async void MatchingStart()
        {
            SteamNetworkManager.Manager.RemoteSteamIdString = Constant.SteamNetworkingType.REMOTESTEAMID;
            await webSocketClient.StartConnect();
            //SteamNetworkManager.Manager.StartP2P();
        }

        public void Logout()
        {
            Authentication.Authentication.logout(LoginManager.Manager.GetTokens().refreshToken);
        }
    }
}