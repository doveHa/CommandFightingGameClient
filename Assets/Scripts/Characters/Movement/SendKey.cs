using System;
using Handler;
using UnityEngine;
using UnityEngine.InputSystem;
using Manager;

public class SendKey : MonoBehaviour
{
    private Vector2 moveDirection;
    private Animator animator;
    private bool jumpKeyInput;

    private string skillName = string.Empty;

    void Awake()
    {
    }

    void Start()
    {
        InputActionManager.Manager.Inputs.Inputs.Move.started += PerformKeyInput;
        InputActionManager.Manager.Inputs.Inputs.Move.canceled += CancelKeyInput;
        InputActionManager.Manager.Inputs.Inputs.Jump.performed += JumpKeyInput;
        InputActionManager.Manager.Inputs.Inputs.Guard.performed += GuardKeyInput;
        InputActionManager.Manager.Inputs.Inputs.Guard.canceled += GuardKeyInputCancel;


        animator = GetComponentInChildren<Animator>();
    }

    public void SetSkillName(string skillName)
    {
        this.skillName = skillName;
    }

    private void PerformKeyInput(InputAction.CallbackContext ctx)
    {
        animator.SetBool("IsMove", true);
        moveDirection = ctx.ReadValue<Vector2>();
    }

    private void CancelKeyInput(InputAction.CallbackContext ctx)
    {
        moveDirection = Vector2.zero;
        animator.SetBool("IsMove", false);
    }

    private void JumpKeyInput(InputAction.CallbackContext ctx)
    {
        jumpKeyInput = true;
    }

    private void GuardKeyInput(InputAction.CallbackContext ctx)
    {
        VarManager.Manager.Player.IsGuard = true;
    }

    private void GuardKeyInputCancel(InputAction.CallbackContext ctx)
    {
        VarManager.Manager.Player.IsGuard = false;
    }

    void FixedUpdate()
    {
        int playerMovement = PlayerMovement();

        RollbackManager.Manager.AdvanceFrame(playerMovement, jumpKeyInput, SkillMapping(skillName));

        if (jumpKeyInput == false && playerMovement == 0 && skillName == string.Empty)
        {
            return;
        }

        SteamNetworkManager.Manager.SendMsg(
            SteamNetworkManager.Manager.RemoteSteamId,
            Constant.SteamNetworkingType.KEYINPUT,
            SendMovementInputFormatting()
        );

        jumpKeyInput = false;
        skillName = string.Empty;
    }

    private int PlayerMovement()
    {
        if (moveDirection.x > 0)
        {
            return 1;
        }

        if (moveDirection.x < 0)
        {
            return -1;
        }

        return 0;
    }

    private string SendMovementInputFormatting()
    {
        return Constant.SteamNetworkingType.KeyInput.MOVEMENT.ToString()
               + Constant.SteamNetworkingType.DELIMITER
               + RollbackManager.Manager.CurrentFrame
               + Constant.SteamNetworkingType.DELIMITER
               + jumpKeyInput
               + Constant.SteamNetworkingType.DELIMITER
               + (-1 * PlayerMovement())
               + Constant.SteamNetworkingType.DELIMITER
               + VarManager.Manager.PlayerGameObject.transform.GetChild(0).localPosition.x
               + Constant.SteamNetworkingType.DELIMITER
               + VarManager.Manager.PlayerGameObject.transform.GetChild(0).localPosition.y
               + Constant.SteamNetworkingType.DELIMITER
               + SkillMapping(skillName);
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
}