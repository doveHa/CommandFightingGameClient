using Handler;
using UnityEngine;
using UnityEngine.InputSystem;
using Manager;
using RollbackNetCode;

public class SendKey : MonoBehaviour
{
    private Vector2 _moveDirection;
    private Player _player;
    private bool _isMove;

    void Awake()
    {
        _player = GetComponent<Player>();
        InputActionManager.Manager.Inputs.Movement.Move.started += PerformKeyInput;
        InputActionManager.Manager.Inputs.Movement.Move.canceled += CancelKeyInput;
        InputActionManager.Manager.Inputs.Movement.Jump.performed += JumpKeyInput;
    }

    private void PerformKeyInput(InputAction.CallbackContext ctx)
    {
        _moveDirection = ctx.ReadValue<Vector2>();
        if ((_moveDirection.x > 0 && !_player.IsLeft())
            || (_moveDirection.x < 0 && _player.IsLeft()))
        {
            _player.SetGuard(true);
        }
        else
        {
            _player.SetGuard(false);
        }

        _isMove = true;
    }

    private void CancelKeyInput(InputAction.CallbackContext ctx)
    {
        _moveDirection = Vector2.zero;
        _player.SetGuard(false);
        _isMove = false;
    }

    private void JumpKeyInput(InputAction.CallbackContext ctx)
    {
        Player player = GetComponent<Player>();
        if (!player.IsJumping)
        {
            player.IsJumping = true;
            Rigidbody2D body = gameObject.transform.GetComponentInChildren<Rigidbody2D>();
            body.AddForce(Vector2.up * ConstController.Manager.JumpForce, ForceMode2D.Impulse);
        }
    }

    void Update()
    {
        int input = 0;
        if (_moveDirection.x > 0)
        {
            input = 1;
        }
        else if (_moveDirection.x < 0)
        {
            input = -1;
        }

        RollbackManager.Manager.AdvanceFrame(input);
        SteamNetworkManager.Manager.SendMsg(
            ulong.Parse(SteamNetworkManager.Manager.RemoteSteamIdString),
            Constant.SteamNetworkingType.MOVEMENT,
            RollbackManager.Manager.CurrentFrame + " " + input);
    }
}