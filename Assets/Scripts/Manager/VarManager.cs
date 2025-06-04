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

        public Dictionary<int, ICharacterSkill> PlayerSkills { get; set; }
        public Dictionary<int, ICharacterSkill> OpponentSkills { get; set; }

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
            Player = PlayerGameObject.GetComponent<Player>();
            Player.Initialize();
            Opponent = OpponentGameObject.GetComponent<Player>();
            Opponent.Initialize();
        }

        private void SetSkills()
        {
            PlayerSkills = new Dictionary<int, ICharacterSkill>();
            OpponentSkills = new Dictionary<int, ICharacterSkill>();
            SetSkill(PlayerCharacterName, Player, PlayerSkills);
            SetSkill(OpponentCharacterName, Opponent, OpponentSkills);
        }

        private void SetSkill(string characterName, Player player, Dictionary<int, ICharacterSkill> skills)
        {
            skills.Add(Constant.SkillName.PUNCH, player.GetComponentInChildren<Punch>());
            skills.Add(Constant.SkillName.JUMP_PUNCH, player.GetComponentInChildren<Punch>());
            
            switch (characterName)
            {
                case "Naktis":
                    skills.Add(Constant.SkillName.Naktis.HASEGI, player.GetComponentInChildren<Hasegi>());
                    skills.Add(Constant.SkillName.Naktis.SCRATCH, player.GetComponentInChildren<Scratch>());
                    skills.Add(Constant.SkillName.Naktis.UPPERWING, player.GetComponentInChildren<UpperWing>());
                    skills.Add(Constant.SkillName.Naktis.FLY, player.GetComponentInChildren<Fly>());
                    break;
                case "Kagetus":

                    break;
                case "Vargon":

                    break;
            }
        }

        public static int SkillMapping(string str)
        {
            Debug.Log(str);
            return str switch
            {
                "Atk_Punch" => Constant.SkillName.PUNCH,
                "Hasegi" => Constant.SkillName.Naktis.HASEGI,
                "Scratch" => Constant.SkillName.Naktis.SCRATCH,
                "UpperWing" => Constant.SkillName.Naktis.UPPERWING,
                "Atk_Kick" => Constant.SkillName.PUNCH,
                "Jumping_Attack" => Constant.SkillName.JUMP_PUNCH,
            };
        }
    }
}


//private Player player;
//private Animator playerAnimator;
//private Player opponent;
//private Animator opponentAnimator;