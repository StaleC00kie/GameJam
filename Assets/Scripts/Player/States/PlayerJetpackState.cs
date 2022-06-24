using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJetpackState : PlayerBaseState
{
    [SerializeField]
    private float moveSpeed = 5;

    private bool heldMove;

    [SerializeField]
    private float jetpackAcceleration = 1.0f; //accelerates n units per second, per second

    [SerializeField]
    public float maxJetpackVelocity = 5.0f;

    public override void SetupInputs(PlayerController _playerController)
    {
        playerController = _playerController;
    }

    public override void Enter(PlayerController _playerController)
    {

    }
    public override void LogicUpdate(PlayerController _playerController)
    {
        playerController.velocity.y += jetpackAcceleration * Time.deltaTime;

        if(playerController.velocity.y > maxJetpackVelocity)
        {
            playerController.velocity.y = maxJetpackVelocity;
        }


        // WASD Movement
        playerController.move = playerController.playerControls.Player.Move.ReadValue<Vector2>();

        float xMove = playerController.move.x * moveSpeed;
        float zMove = playerController.move.y * moveSpeed;

        float tempY = playerController.velocity.y;

        playerController.velocity = (transform.right * xMove) + (transform.forward * zMove);

        playerController.velocity.y = tempY;


        // Rotate player's body to align with the players camera
        transform.eulerAngles = new Vector3(transform.rotation.x, playerController.playerCamera.transform.eulerAngles.y, transform.rotation.z);

        playerController.cc.Move(playerController.velocity * Time.deltaTime);
    }

    public void Gravity()
    {
        if (playerController.cc.isGrounded && playerController.velocity.y < 0)
        {
            playerController.velocity.y = -2.0f;
        }

        playerController.velocity.y += playerController.gravity * Time.deltaTime;
    }

    public override void DelayedUpdate(PlayerController _playerController)
    {


    }


    public override void PhysicsUpdate(PlayerController _playerController)
    {

    }

    public override void CheckStateTransitions(PlayerStateManager stateMachine)
    {
        if (playerController.playerControls.Player.Jump.phase == InputActionPhase.Waiting)
        {
            stateMachine.ChangeState(stateMachine.playerMoveState);
        }
    }

    public override void RemoveInputs()
    {

    }

    public override void Exit(PlayerController _playerController)
    {

    }
}
