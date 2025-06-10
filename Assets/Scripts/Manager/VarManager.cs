using System.Collections.Generic;
using Characters;
using Characters.Skill;
using Characters.Skill.Kagetsu;
using Characters.Skill.Naktis;
using Manager;
using UnityEngine;
using UnityEngine.UI;

public class VarManager : MonoBehaviour
{
    public static VarManager Manager;

    public Player Player { get; set; }
    public Player Opponent { get; set; }

    public GameObject PlayerGameObject { get; set; }
    public GameObject OpponentGameObject { get; set; }

    public string PlayerCharacterName { get; set; }
    public string OpponentCharacterName { get; set; }

    // 여기 추가
    public string PlayerCharacterImage { get; set; }
    public string OpponentCharacterImage { get; set; }

    public HitBoxHandler PlayerHitBoxHandler { get; set; }
    public HitBoxHandler OpponentHitBoxHandler { get; set; }

    public Dictionary<int, ICharacterSkill> PlayerSkills { get; set; }
    public Dictionary<int, ICharacterSkill> OpponentSkills { get; set; }


    private void Awake()
    {
        if (Manager == null)
        {
            Manager = this;
        }
    }
    public void Update()
    {
        Debug.Log("작동중");
    }

    public void PlayerOpponentInitialize()
    {
        SetComponents();
        SetDataSets();
        SetSkills();
        SetHitBoxHandler();
        LoadCharacterImages();
    }

    private void SetComponents()
    {
        Player = PlayerGameObject.GetComponentInParent<Player>();
        Player.Initialize();
        Opponent = OpponentGameObject.GetComponentInParent<Player>();
        Opponent.Initialize();
    }

    private void SetDataSets()
    {
        Player.SetDataSet(PlayerCharacterName);
        Opponent.SetDataSet(OpponentCharacterName);

        // 이미지 이름을 캐릭터 이름 기반으로 자동 설정 예시
        PlayerCharacterImage = PlayerCharacterName + ".png";
        OpponentCharacterImage = OpponentCharacterName + "(spin).png";
    }

    // (선택) 이미지 리소스를 불러오는 함수
    private void LoadCharacterImages()
    {
        Texture2D playerImage = LoadImage(PlayerCharacterImage);
        Texture2D opponentImage = LoadImage(OpponentCharacterImage);

        if (playerImage != null && GameManager.Manager.playerImageUI != null)
        {
            GameManager.Manager.playerImageUI.texture = playerImage;
            var rt = GameManager.Manager.playerImageUI.rectTransform;
            rt.sizeDelta = new Vector2(200f, 200f);
            rt.anchoredPosition = new Vector2(-896f, 386f);
        }

        if (opponentImage != null && GameManager.Manager.opponentImageUI != null)
        {
            GameManager.Manager.opponentImageUI.texture = opponentImage;
            var rt = GameManager.Manager.opponentImageUI.rectTransform;
            rt.sizeDelta = new Vector2(200f, 200f);
            rt.anchoredPosition = new Vector2(896f, 386f);
        }
    }


    private Texture2D LoadImage(string fileName)
    {
        string path = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);
        if (System.IO.File.Exists(path))
        {
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(bytes);
            return texture;
        }
        else
        {
            Debug.LogWarning("이미지 파일이 존재하지 않습니다: " + path);
            return null;
        }
    }


    private void SetSkills()
        {
            PlayerSkills = new Dictionary<int, ICharacterSkill>();
            OpponentSkills = new Dictionary<int, ICharacterSkill>();
            SetSkill(PlayerCharacterName, Player, PlayerSkills);
            SetSkill(OpponentCharacterName, Opponent, OpponentSkills);
        }

        private void SetHitBoxHandler()
        {
            PlayerHitBoxHandler = PlayerGameObject.GetComponentInParent<HitBoxHandler>();
            OpponentHitBoxHandler = OpponentGameObject.GetComponentInParent<HitBoxHandler>();
        }

        private void SetSkill(string characterName, Player player, Dictionary<int, ICharacterSkill> skills)
        {
            skills.Add(Constant.SkillName.PUNCH, player.GetComponentInChildren<Punch>());
            skills.Add(Constant.SkillName.JUMP_PUNCH, player.GetComponentInChildren<JumpPunch>());

            SkillGroup skillGroup = CharacterManager.Manager.CharacterGroup.Characters[characterName].SkillGroup;
            switch (characterName)
            {
                case "Naktis":
                    skills.Add(Constant.SkillName.Naktis.HASEGI, player.GetComponentInChildren<Hasegi>());
                    player.GetComponentInChildren<Hasegi>().SetCommandCoff(skillGroup.Skills["바람강타"].Command.Count);
                    skills.Add(Constant.SkillName.Naktis.SCRATCH, player.GetComponentInChildren<Scratch>());
                    player.GetComponentInChildren<Scratch>().SetCommandCoff(skillGroup.Skills["할퀴기"].Command.Count);
                    skills.Add(Constant.SkillName.Naktis.UPPERWING, player.GetComponentInChildren<UpperWing>());
                    player.GetComponentInChildren<UpperWing>()
                        .SetCommandCoff(skillGroup.Skills["어퍼윙"].Command.Count);
                    skills.Add(Constant.SkillName.Naktis.FLY, player.GetComponentInChildren<Fly>());
                    player.GetComponentInChildren<Fly>().SetCommandCoff(skillGroup.Skills["비행"].Command.Count);
                    break;
                case "Kagetsu":
                    skills.Add(Constant.SkillName.Kagetsu.Nageru, player.GetComponentInChildren<Nageru>());
                    player.GetComponentInChildren<Nageru>().SetCommandCoff(skillGroup.Skills["잡기"].Command.Count);
                    skills.Add(Constant.SkillName.Kagetsu.Sangiri, player.GetComponentInChildren<Sangiri>());
                    player.GetComponentInChildren<Sangiri>().SetCommandCoff(skillGroup.Skills["3단 베기"].Command.Count);
                    skills.Add(Constant.SkillName.Kagetsu.NageKunai, player.GetComponentInChildren<NageKunai>());
                    player.GetComponentInChildren<NageKunai>().SetCommandCoff(skillGroup.Skills["쿠나이"].Command.Count);
                    skills.Add(Constant.SkillName.Kagetsu.IttoRyotan, player.GetComponentInChildren<IttoRyotan>());
                    player.GetComponentInChildren<IttoRyotan>().SetCommandCoff(skillGroup.Skills["베기"].Command.Count);
                    break;
                case "Vargon":

                    break;
            }
        }

        public static int SkillMapping1(string skillName)
        {
            return skillName switch
            {
                "공격" => Constant.SkillName.PUNCH,
                "추가타" => Constant.SkillName.PUNCH,
                "점프공격" => Constant.SkillName.JUMP_PUNCH,

                "바람강타" => Constant.SkillName.Naktis.HASEGI,
                "할퀴기" => Constant.SkillName.Naktis.SCRATCH,
                "어퍼윙" => Constant.SkillName.Naktis.UPPERWING,
                "비행" => Constant.SkillName.Naktis.FLY,

                "잡기" => Constant.SkillName.Kagetsu.Nageru,
                "3단 베기" => Constant.SkillName.Kagetsu.Sangiri,
                "쿠나이" => Constant.SkillName.Kagetsu.NageKunai,
                "베기" => Constant.SkillName.Kagetsu.IttoRyotan,

                "내려찍기" => Constant.SkillName.Vargon.Slam,
                "돌진" => Constant.SkillName.Vargon.Rush,
                "웅크리기" => Constant.SkillName.Vargon.Curl,
                "당기기" => Constant.SkillName.Vargon.Grab
            };

            return 0;
        }

        public static int StateToSkill(string state)
        {
            return state switch
            {
                "Atk_Punch" => Constant.SkillName.PUNCH,
                "Hasegi" => Constant.SkillName.Naktis.HASEGI,
                "Scratch" => Constant.SkillName.Naktis.SCRATCH,
                "UpperWing" => Constant.SkillName.Naktis.UPPERWING,
                "Atk_Kick" => Constant.SkillName.PUNCH,
                "Jumping_Attack" => Constant.SkillName.JUMP_PUNCH,
                _ => Constant.SkillName.NONE
            };
        }
    }
