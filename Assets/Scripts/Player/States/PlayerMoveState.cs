using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveState : PlayerBaseState
{
    [SerializeField]
    private float moveSpeed = 5;

    private bool heldMove;

    private bool look;
    public override void SetupInputs(PlayerController _playerController)
    {
        playerController = _playerController;

        playerController.playerControls.Player.Move.performed += Move;
        playerController.playerControls.Player.Move.canceled += MoveCanceled;

        playerController.playerControls.Player.Look.performed += Look;
    }
    public override void Enter(PlayerController _playerController)
    {

    }

    public void Move(InputAction.CallbackContext context)
    {
        heldMove = true;
    }

    public void MoveCanceled(InputAction.CallbackContext context)
    {
        heldMove = false;
    }
    public void Look(InputAction.CallbackContext context)
    {
        look = true;
    }

    public override void LogicUpdate(PlayerController _playerController)
    {
        Gravity();

        if (heldMove)
        {
            playerController.move = playerController.playerControls.Player.Move.ReadValue<Vector2>() * Time.deltaTime;

            float xMove = playerController.move.x * moveSpeed;
            float zMove = playerController.move.y * moveSpeed;

            float tempY = playerController.velocity.y;

            playerController.velocity = (transform.right * xMove) + (transform.forward * zMove);

            playerController.velocity.y = tempY;
        }
        else
        {
            playerController.move = Vector2.zero;

            float xMove = playerController.move.x * moveSpeed;
            float zMove = playerController.move.y * moveSpeed;

            float tempY = playerController.velocity.y;

            playerController.velocity = (transform.right * xMove) + (transform.forward * zMove);

            playerController.velocity.y = tempY;
        }

        transform.eulerAngles = new Vector3(transform.rotation.x, playerController.playerCamera.transform.eulerAngles.y, transform.rotation.z);

        playerController.cc.Move(playerController.velocity);
    }

    public void Gravity()
    {
        playerController.velocity.y += playerController.gravity * Time.deltaTime;
    }

    public override void DelayedUpdate(PlayerController _playerController)
    {


    }


    public override void PhysicsUpdate(PlayerController _playerController)
    {

    }

    public override void Exit(PlayerController _playerController)
    {

    }
}
