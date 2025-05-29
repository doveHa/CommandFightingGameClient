using System.Collections.Generic;
using Characters;
using Characters.Skill;
using Characters.Skill.Naktis;
using UnityEngine;

namespace Manager
{
    public class VarManager : MonoBehaviour
    {
        public static VarManager Manager;

        public Player Player { get; set; }
        public Player Opponent { get; set; }

        public GameObject PlayerGameObject { get; set; }
        public GameObject OpponentGameObject { get; set; }

        public string PlayerCharacterName { get; set; }
        public string OpponentCharacterName { get; set; }

        public Dictionary<string, ICharacterSkill> PlayerSkills { get; set; }
        public Dictionary<string, ICharacterSkill> OpponentSkills { get; set; }

        private void Awake()
        {
            if (Manager == null)
            {
                Manager = this;
            }
        }

        public void PlayerOpponentInitialize()
        {
            SetComponents();
            SetDataSets();
            SetSkills();
        }

        private void SetDataSets()
        {
            Player.SetDataSet(PlayerCharacterName);
            Opponent.SetDataSet(OpponentCharacterName);
        }

        private void SetComponents()
        {
            Player = PlayerGameObject.GetComponentInChildren<Player>();
            Opponent = OpponentGameObject.GetComponentInChildren<Player>();
        }

        private void SetSkills()
        {
            PlayerSkills = new Dictionary<string, ICharacterSkill>();
            OpponentSkills = new Dictionary<string, ICharacterSkill>();
            SetSkill(PlayerCharacterName, Player, PlayerSkills);
            SetSkill(OpponentCharacterName, Opponent, OpponentSkills);
        }

        private void SetSkill(string characterName, Player player, Dictionary<string, ICharacterSkill> skills)
        {
            skills.Add("Atk_Punch", player.GetComponentInChildren<Punch>());
            switch (characterName)
            {
                case "Naktis":
                    skills.Add("Hasegi", player.GetComponentInChildren<Hasegi>());
                    skills.Add("Scratch", player.GetComponentInChildren<Scratch>());
                    skills.Add("UpperWing", player.GetComponentInChildren<UpperWing>());
                    skills.Add("Fly", player.GetComponentInChildren<Fly>());
                    break;
                case "Kagetus":

                    break;
                case "Vargon":

                    break;
            }
        }
    }
}


//private Player player;
//private Animator playerAnimator;
//private Player opponent;
//private Animator opponentAnimator;