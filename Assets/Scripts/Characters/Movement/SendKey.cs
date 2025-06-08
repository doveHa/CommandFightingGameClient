/*
using System;
using System.Text;
using Handler;
using UnityEngine;
using UnityEngine.InputSystem;
using Manager;
using Movement;
using RollbackNetcode;

public class SendKey : MonoBehaviour
{
    private SetMove setMove;
    private SetJump setJump;
    private SetActive setActive;

    void Start()
    {
        setMove = new SetMove();
        setJump = new SetJump();
        setActive = new SetActive();
    }

    void FixedUpdate()
    {
        string sendMsg = SendMsg(setMove.MoveSet(), setJump.JumpSet(), setActive.ActiveSet());
        setJump.Initialize();
        setActive.Initialize();
        SteamNetworkManager.Manager.SendMsg(SteamNetworkManager.Manager.RemoteSteamId,
            Constant.SteamNetworkingType.KEYINPUT, sendMsg);
    }

    private string SendMsg(int move, bool jump, int active)
    {
        StringBuilder builder = new StringBuilder();
        builder
            .Append(RollbackManager.Manager.CurrentFrame)
            .Append(Constant.SteamNetworkingType.DELIMITER)
            .Append(-1 * move)
            .Append(Constant.SteamNetworkingType.DELIMITER)
            .Append(jump)
            .Append(Constant.SteamNetworkingType.DELIMITER)
            .Append(active);
        return builder.ToString();
    }

    private int SkillMapping(string skillName)
    {
        switch (skillName)
        {
            case "Atk_Punch":
                return Constant.SkillName.PUNCH;
            case "Jumping_Attack":
                return Constant.SkillName.JUMP_PUNCH;
            case "어퍼윙":
                return Constant.SkillName.Naktis.UPPERWING;
            case "바람강타":
                return Constant.SkillName.Naktis.HASEGI;
            case "비행":
                return Constant.SkillName.Naktis.FLY;
            case "할퀴기":
                return Constant.SkillName.Naktis.SCRATCH;
            case "잡기":
                return Constant.SkillName.Kagetsu.Nageru;
            case "3단 베기":
                return Constant.SkillName.Kagetsu.Sangiri;
            case "베기":
                return Constant.SkillName.Kagetsu.IttoRyotan;
            case "쿠나이":
                return Constant.SkillName.Kagetsu.NageKunai;
            default:
                return -1;
        }
    }
}*/