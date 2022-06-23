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
    public override void SetupInputs()
    {
        playerControls.Player.Move.performed += Move;
        playerControls.Player.Move.canceled += MoveCanceled;

        playerControls.Player.Look.performed += Look;
    }
    public override void Enter(PlayerController _playerController)
    {
        playerController = _playerController;
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
            playerController.move = playerControls.Player.Move.ReadValue<Vector2>() * Time.deltaTime;

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

        if(look)
        {
            playerController.mouse = playerControls.Player.Look.ReadValue<Vector2>() * playerController.mouseSensitivity * Time.deltaTime;

            // Rotate camera up and down
            playerController.xRot -= playerController.mouse.y;

            playerController.xRot = Mathf.Clamp(playerController.xRot, -90f, 90f);
            playerController.virtualCamera.transform.localEulerAngles = new Vector3(playerController.xRot, 0, 0);

            // Rotate player body left and right
            transform.Rotate(Vector3.up * playerController.mouse.x);

            look = false;
        }

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
