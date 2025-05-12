using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Characters;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Handler
{
    public class CharacterSelectHandler : MonoBehaviour
    {
        [SerializeField] private GameObject description;
        [SerializeField] private Image[] images;
        [SerializeField] private GameObject[] buttons;
        [SerializeField] private GameObject[] changeCommandSkills;

        [SerializeField] private TextMeshProUGUI skillName, skillDescription;

        public static string CurrentShowCharacter { get; private set; }
        public static string CurrentShowSkill { get; private set; }
        public static string[] skillNames { get; private set; }
        
        private Color defaultColor, selectCharacterColor;
        
        private Image[] skillIcons;
        private TextMeshProUGUI[] commands;

        void Start()
        {
            defaultColor = images[0].color;
            ColorUtility.TryParseHtmlString("#4690F0", out selectCharacterColor);
            skillIcons = new Image[changeCommandSkills.Length];
            commands = new TextMeshProUGUI[changeCommandSkills.Length];
            skillNames = new string[4];
        }

        //캐릭터 선택
        public void OnClickCharacterInfo(int index)
        {
            AllDescriptionOff();
            images[index].color = selectCharacterColor;
            switch (index)
            {
                case 0:
                    CurrentShowCharacter = "Naktis";
                    break;
                case 1:
                    CurrentShowCharacter = "Kagetsu";
                    break;
                case 2:
                    CurrentShowCharacter = "Vargon";
                    break;
            }

            SkillIconLoad();
            ChangeCommandInfoLoad();
            description.transform.GetChild(index).gameObject.SetActive(true);
        }

        //캐릭터 설명 (Description GameObject) 초기화
        public void AllDescriptionOff()
        {
            for (int i = 0; i < description.transform.childCount; i++)
            {
                images[i].color = defaultColor;
                description.transform.GetChild(i).gameObject.SetActive(false);
            }

            foreach (var button in buttons)
            {
                button.SetActive(false);
            }

            skillName.text = string.Empty;
            skillDescription.text = string.Empty;
        }

        //스킬 버튼 아이콘 로드
        private void SkillIconLoad()
        {
            foreach (var button in buttons)
            {
                button.SetActive(true);
            }

            ICharacter character = CurrentCharacter();
            List<ISkill> skills = character.SkillGroup.Skills.Values.ToList();
            for (int i = 0; i < skills.Count; i++)
            {
                buttons[i].GetComponent<Image>().sprite =
                    Resources.Load<Sprite>("Images/Icon/SkillIcon/" + skills[i].Name);
            }
        }
        
        //메인의 스킬 아이콘, 커맨드를 로드
        private void ChangeCommandInfoLoad()
        {
            ICharacter character = CurrentCharacter();

            for (int i = 0; i < skillIcons.Length; i++)
            {
                skillIcons[i] = changeCommandSkills[i].GetComponent<Image>();
                commands[i] = changeCommandSkills[i].GetComponentInChildren<TextMeshProUGUI>();
            }

            List<ISkill> skills = character.SkillGroup.Skills.Values.ToList();
            for (int i = 0; i < skills.Count; i++)
            {
                skillIcons[i].sprite = Resources.Load<Sprite>("Images/Icon/SkillIcon/" + skills[i].Name);
                skillNames[i] = skills[i].Name;
                commands[i].text = CommandListToString(skills[i].Command);
                
            }
        }
        
        //스킬 아이콘 클릭 시 스킬 이름 및 설명 로드
        public void OnClickSkillIcon(int index)
        {
            CharacterManager.Manager.CharacterGroup.Characters.TryGetValue(CurrentShowCharacter, out var character);
            ISkill skill = character.SkillGroup.Skills.Values.ToList()[index];
            CurrentShowSkill = skill.Name;
            skillName.text = skill.Name;
            skillDescription.text = skill.Description;
        }
        
        //커맨드 키를 char로 변환
        public static char CommandToCharacter(string command)
        {
            switch (command)
            {
                case "UPARROW":
                    return '↑';
                case "DOWNARROW":
                    return '↓';
                case "LEFTARROW":
                    return '←';
                case "RIGHTARROW":
                    return '→';
                default:
                    return command.ToCharArray()[0];
            }
        }

        //커맨드 List<string>을 string으로 변환
        private static string CommandListToString(List<string> commands)
        {
            string sumCommand = "";
            foreach (string command in commands)
            {
                sumCommand += CommandToCharacter(command) + " ";
            }

            return sumCommand;
        }

        //현재 선택중인 캐릭터
        private ICharacter CurrentCharacter()
        {
            CharacterManager.Manager.CharacterGroup.Characters.TryGetValue(CurrentShowCharacter,
                out ICharacter character);
            return character;
        }
        
        //커맨드 변경 후 다시 커맨드 로드
        public void ReLoadCommand()
        {
            ICharacter character = CurrentCharacter();

            List<ISkill> skills = character.SkillGroup.Skills.Values.ToList();

            for (int i = 0; i < skills.Count; i++)
            {
                commands[i].text = CommandListToString(skills[i].Command);
            }
        }
    }
}