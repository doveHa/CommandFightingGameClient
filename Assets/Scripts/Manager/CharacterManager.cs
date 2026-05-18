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
        public static CharacterManager Manager { get; private set; }

        private ComboInputHandler comboInputHandler;

        public CharacterGroup CharacterGroup { get; private set; }

        public CharacterSelectHandler handler;
        public void CharacterOn(string characterName)
        {
            comboInputHandler.AddCharacterCombo(characterName);

            GameObject characterSet = GameObject.Find("Character");
            for (int i = 0; i < characterSet.transform.childCount; i++)
            {
                characterSet.transform.GetChild(i).gameObject.SetActive(false);
            }

            characterSet.transform.Find(characterName).gameObject.SetActive(true);
        }

        void Awake()
        {
            if (Manager != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Manager = this;
            }

            CharacterGroup = new CharacterGroup();
        }

        async void Start()
        {
            comboInputHandler = GameObject.Find("Manager").GetComponent<ComboInputHandler>();
            await Initialize();
            
            CharacterOn("Kagetsu");
            handler.ChangeCommandInfoLoad("Kagetsu");
        }

        private async Task Initialize()
        {
            await GetCharacter();
            await GetCustomCommand();
            CharacterGroup.InitializeCurrentCommandList();
        }

        private async Task GetCharacter()
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

        private async Task GetCustomCommand()
        {
            RestResponse response = await RestAPIRequest.Get<GetCommandDTO>(Constant.RestAPI.CustomCommand.ALL, null,
                LoginManager.Manager.GetAuthHeader());
            Debug.Log(response.Content);
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