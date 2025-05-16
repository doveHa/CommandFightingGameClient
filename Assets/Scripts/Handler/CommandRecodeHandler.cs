using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Characters;
using DTO;
using Manager;
using Server;
using RestSharp;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Handler
{
    public class CommandRecodeHandler : MonoBehaviour
    {
        [SerializeField] private CharacterSelectHandler characterSelectHandler;
        public Dictionary<string, List<string>> ChangeCommandList { get; set; }
        public Dictionary<string, List<string>> UpdateCommandLIst { get; set; }
        private ComboInputHandler comboInputHandler;
        private TextMeshProUGUI currentChangeCommand;
        private List<string> recode;
        private bool isRecode, isChanged;

        IEnumerator Start()
        {
            yield return new WaitUntil(() =>
                CharacterManager.Manager.CharacterGroup.CurrentCommandList != null);

            ChangeCommandList = DeepCopy(CharacterManager.Manager.CharacterGroup.CurrentCommandList);
            recode = new List<string>();
            isChanged = false;
            comboInputHandler = GameObject.Find("Manager").GetComponent<ComboInputHandler>();
        }

        public void OnClickRecode(GameObject skill)
        {
            TextMeshProUGUI buttonText = skill.transform.Find("Button").GetComponentInChildren<TextMeshProUGUI>();
            currentChangeCommand = skill.transform.Find("Command").GetComponent<TextMeshProUGUI>();
            currentChangeCommand.text = "";

            if (!isRecode)
            {
                buttonText.text = "완료";
                StartRecode();
            }
            else
            {
                buttonText.text = "변경";
                StopRecode(skill.name);
            }
        }

        public void StartRecode()
        {
            if (isRecode)
            {
                return;
            }

            isRecode = true;
            InputActionManager.Manager.Inputs.Command.CommandInput.started += CommandRecoding;
        }

        public void StopRecode(string gameObjectName)
        {
            InputActionManager.Manager.Inputs.Command.CommandInput.started -= CommandRecoding;
            string currentCharacterName = CharacterSelectHandler.CurrentShowCharacter;
            string currentSkillName = GetSkillName(gameObjectName);
            string key = currentCharacterName + "+" + currentSkillName;
            ChangeCommandList[key] = recode.ToList();
            CharacterManager.Manager.CharacterGroup.ChangeCommand(currentCharacterName, currentSkillName,
                recode.ToList());
            characterSelectHandler.ReLoadCommand();
            comboInputHandler.AddCombo(CharacterManager.Manager.CharacterGroup.Characters[currentCharacterName]
                .SkillGroup.Skills[currentSkillName]);
            recode.Clear();
            isRecode = false;
        }


        public async void SendCommand()
        {
            if (isChanged)
            {
                List<SetCommandDTO> sendCommands = CommandDictionaryToList(ChangeCommandList);

                var json = JsonSerializer.Serialize(sendCommands);
                Debug.Log(json);

                RestResponse response = await RestAPIRequest.Post(Constant.RestAPI.CustomCommand.SET, sendCommands,
                    LoginManager.Manager.GetAuthHeader());

                if (response.IsSuccessful)
                {
                    CharacterManager.Manager.CharacterGroup.CurrentCommandList = DeepCopy(ChangeCommandList);
                }

                isChanged = false;
            }
        }

        private void CommandRecoding(InputAction.CallbackContext context)
        {
            isChanged = true;
            currentChangeCommand.text +=
                CharacterSelectHandler.CommandToCharacter(context.control.name.ToUpper()) + " ";
            recode.Add(context.control.name.ToUpper());
        }

        private List<SetCommandDTO> CommandDictionaryToList(Dictionary<string, List<string>> commandList)
        {
            List<SetCommandDTO> list = new List<SetCommandDTO>();
            foreach (KeyValuePair<string, List<string>> keyValuePair in commandList)
            {
                string[] keys = keyValuePair.Key.Split('+');
                list.Add(new SetCommandDTO(keys[0], keys[1], keyValuePair.Value));
            }

            return list;
        }

        private Dictionary<string, List<string>> DeepCopy(Dictionary<string, List<string>> original)
        {
            Dictionary<string, List<string>> copy = new Dictionary<string, List<string>>();
            foreach (KeyValuePair<string, List<string>> command in original)
            {
                copy.Add(command.Key, command.Value);
            }

            return copy;
        }

        private string GetSkillName(string skill)
        {
            Debug.Log(CharacterSelectHandler.skillNames[int.Parse(skill.Substring("SKill".Length)) - 1]);
            return CharacterSelectHandler.skillNames[int.Parse(skill.Substring("SKill".Length)) - 1];
        }
    }
}