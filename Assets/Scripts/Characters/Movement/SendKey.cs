using System;
using Handler;
using UnityEngine;
using UnityEngine.InputSystem;
using Manager;

public class SendKey : MonoBehaviour
{
    private Vector2 moveDirection;

    private Player player;

    //private bool isMove;
    [SerializeField] private Animator animator;
    private bool jumpKeyInput;

    private string skillName = string.Empty;
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

    public void SetSkillName(string skillName)
    {
        this.skillName = skillName;
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

        Debug.Log("JUMP &" + PlayerMovement());
        /*
        SteamNetworkManager.Manager.SendMsg(
            SteamNetworkManager.Manager.RemoteSteamId,
            Constant.SteamNetworkingType.KEYINPUT,
            SendMovementInputFormatting(Constant.SteamNetworkingType.KeyInput.MOVEMENT)
        );*/
    }

    void FixedUpdate()
    {
        int playerMovement = PlayerMovement();

        RollbackManager.Manager.AdvanceFrame(playerMovement, jumpKeyInput, skillName);

        if (jumpKeyInput == false && playerMovement == 0 && skillName == String.Empty)
        {
            return;
        }

        SteamNetworkManager.Manager.SendMsg(
            SteamNetworkManager.Manager.RemoteSteamId,
            Constant.SteamNetworkingType.KEYINPUT,
            SendMovementInputFormatting()
        );


        jumpKeyInput = false;
        skillName = null;
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
               + skillName;
    }
}