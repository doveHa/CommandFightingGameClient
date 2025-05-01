using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using UnityEngine;
using Server;
using RestSharp;
using DTO;
using Characters;
using Handler;

namespace Manager
{
    public class CharacterManager : MonoBehaviour
    {
        //선택한 캐릭터의 이름만 vs 선택한 캐릭터의 객체 정보
        public string OpponentCharacterName { get; set; }
        public string PlayerCharacterName { get; set; }

        public static CharacterManager Manager { get; private set; }

        public CharacterGroup CharacterGroup;
        
        public void CharacterOn()
        {
            GameObject characterSet = GameObject.Find("Character");
            for (int i = 0; i < characterSet.transform.childCount; i++)
            {
                characterSet.transform.GetChild(i).gameObject.SetActive(false);
            }

            characterSet.transform.Find(PlayerCharacterName).gameObject.SetActive(true);
        }

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (Manager == null)
            {
                Manager = this;
            }
        }

        public async Task Initialize()
        {
            CharacterGroup = new CharacterGroup();
            await GetCharacter();
            await GetCustomCommand();
            CharacterGroup.InitializeCurrentCommandList();
        }
        
        public async Task GetCharacter()
        {
            try
            {
                RestResponse response = await RestAPIRequest.Get<string>(Constant.RestAPI.Character.ALL, null, null);
                Debug.Log(response.Content);
                List<CharacterDTO> characters = JsonSerializer.Deserialize<List<CharacterDTO>>(response.Content);

                foreach (CharacterDTO character in characters)
                {
                    CharacterGroup.Add(character);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                throw;
            }
        }

        public async Task GetCustomCommand()
        {
            RestResponse response = await RestAPIRequest.Get<GetCommandDTO>(Constant.RestAPI.CustomCommand.ALL, null,
                LoginManager.Manager.GetAuthHeader());
            List<GetCommandDTO> allCustomCommand =
                JsonSerializer.Deserialize<List<GetCommandDTO>>(response.Content);
            InitializeCommand(allCustomCommand);
        }

        private void InitializeCommand(List<GetCommandDTO> allCustomCommand)
        {
            foreach (GetCommandDTO customCommand in allCustomCommand)
            {
                string characterName = customCommand.characterName;
                string skillName = customCommand.skillName;
                Debug.Log(skillName);
                CharacterGroup.Characters[characterName].SkillGroup.Skills[skillName].Command = customCommand.command;
            }
        }
    }
}