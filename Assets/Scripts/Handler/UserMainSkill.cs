using Characters.Skill;
using Characters.Skill.Kagetsu;
using Characters.Skill.Naktis;
using Movement;
using UnityEngine;

public class UserMainSkill : MonoBehaviour
{
    public SetActive active;

    void Start()
    {
        active = new SetActive();
    }

    void Update()
    {
        if (SetActive.SkillIndex != Constant.SkillName.NONE)
        {
            RunIndexSkill(SetActive.SkillIndex);
            SetActive.SkillIndex = Constant.SkillName.NONE;
        }
    }

    private void RunIndexSkill(int index)
    {
        switch (index)
        {
            case Constant.SkillName.PUNCH:
                GetComponentInChildren<Punch>().Run(0);
                break;
            case Constant.SkillName.Naktis.SCRATCH:
                GetComponentInChildren<Scratch>().Run(0);
                break;
            case Constant.SkillName.Naktis.UPPERWING:
                GetComponentInChildren<UpperWing>().Run(0);
                break;
            case Constant.SkillName.Naktis.FLY:
                GetComponentInChildren<Fly>().Run(0);
                break;
            case Constant.SkillName.Naktis.HASEGI:
                GetComponentInChildren<Hasegi>().Run(0);
                break;
            case Constant.SkillName.Kagetsu.Sangiri:
                GetComponentInChildren<Sangiri>().Run(0);
                break;
            case Constant.SkillName.Kagetsu.Nageru:
                GetComponentInChildren<Nageru>().Run(0);
                break;
            case Constant.SkillName.Kagetsu.IttoRyotan:
                GetComponentInChildren<IttoRyotan>().Run(0);
                break;
            case Constant.SkillName.Kagetsu.NageKunai:
                GetComponentInChildren<NageKunai>().Run(0);
                break;
        }
    }
}