using System.Collections.Generic;
using Handler;
using UnityEngine;
using UnityEngine.InputSystem;
using Manager;
using RollbackNetCode;

public class SendKey : MonoBehaviour
{
    private Vector2 moveDirection;

    private Player player;

    //private bool isMove;
    [SerializeField] private Animator animator;
    private bool jumpKeyInput = false;

    private static string _skillName = string.Empty;
    //private bool[] skillsInput = new bool[4];

    void Awake()
    {
        player = GetComponent<Player>();
    }

    void Start()
    {
        InputActionManager.Manager.Inputs.Movement.Move.started += PerformKeyInput;
        InputActionManager.Manager.Inputs.Movement.Move.canceled += CancelKeyInput;
        InputActionManager.Manager.Inputs.Movement.Jump.performed += JumpKeyInput;
    }

    public static void SetSkillName(string skillName)
    {
        _skillName = skillName;
    }

    private void PerformKeyInput(InputAction.CallbackContext ctx)
    {
        animator.SetBool("IsMove", true);

        moveDirection = ctx.ReadValue<Vector2>();
        if ((moveDirection.x > 0 && !GameManager.Manager.IsPlayerLeft)
            || (moveDirection.x < 0 && GameManager.Manager.IsPlayerLeft))
        {
            player.SetGuard(true);
        }
        else
        {
            player.SetGuard(false);
        }
        //isMove = true;
    }

    private void CancelKeyInput(InputAction.CallbackContext ctx)
    {
        moveDirection = Vector2.zero;
        player.SetGuard(false);
        animator.SetBool("IsMove", false);

        //isMove = false;
    }

    /*
    private void JumpKeyInput(InputAction.CallbackContext ctx)
    {
        RollbackManager.Manager.AdvanceFrame(true);
        //Movement.CharacterMovementController.JumpCharacter(gameObject);

        SteamNetworkManager.Manager.SendMsg(
            SteamNetworkManager.Manager.RemoteSteamId,
            Constant.SteamNetworkingType.KEYINPUT,
            SendKeyInputFormatting(Constant.SteamNetworkingType.KeyInput.JUMP,
                string.Empty)
        );
    }
*/
    private void JumpKeyInput(InputAction.CallbackContext ctx)
    {
        jumpKeyInput = true;

        SteamNetworkManager.Manager.SendMsg(
            SteamNetworkManager.Manager.RemoteSteamId,
            Constant.SteamNetworkingType.KEYINPUT,
            SendKeyInputFormatting(Constant.SteamNetworkingType.KeyInput.JUMP,
                string.Empty)
        );
    }

    void FixedUpdate()
    {
        int input = 0;
        if (moveDirection.x > 0)
        {
            input = 1;
        }
        else if (moveDirection.x < 0)
        {
            input = -1;
        }

        RollbackManager.Manager.AdvanceFrame(input, jumpKeyInput, _skillName);

        if (input != 0)
        {
            SteamNetworkManager.Manager.SendMsg(
                SteamNetworkManager.Manager.RemoteSteamId,
                Constant.SteamNetworkingType.KEYINPUT,
                SendKeyInputFormatting(Constant.SteamNetworkingType.KeyInput.MOVEMENT, input.ToString())
            );
        }

        jumpKeyInput = false;
        _skillName = null;
    }

    private string SendKeyInputFormatting(int type, string msg)
    {
        return type.ToString()
               + Constant.SteamNetworkingType.DELIMITER
               + RollbackManager.Manager.CurrentFrame
               + Constant.SteamNetworkingType.DELIMITER
               + msg;
    }
}