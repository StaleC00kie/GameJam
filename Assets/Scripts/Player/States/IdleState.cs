using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

[RequireComponent(typeof(PlayerState))]
public class IdleState : PlayerState
{
    public override void Enter()
    {

    }

    public override void LogicUpdate()
    {
        if (controller.isGrounded && inputController.velocity.y < 0)
        {
            inputController.velocity.y = -2.0f;
        }


        if (inputController.moveAction.action.ReadValue<Vector2>() != Vector2.zero)
        {
            stateMachine.ChangeState(moveState);
        }

        Gravity();

        controller.Move(new Vector3(0, inputController.velocity.y, 0) * Time.deltaTime);
    }

    public override void DelayedUpdate()
    {
        Look();
    }

    public void Look()
    {
        inputController.mouse = inputController.lookAction.action.ReadValue<Vector2>() * inputController.mouseSensitivity * Time.deltaTime;

        // Rotate camera up and down
        inputController.xRot -= inputController.mouse.y;

        inputController.xRot = Mathf.Clamp(inputController.xRot, -90f, 90f);
        inputController.virtualCamera.transform.localEulerAngles = new Vector3(inputController.xRot, 0, 0);

        // Rotate player body left and right
        transform.Rotate(Vector3.up * inputController.mouse.x);
    }

    public void Gravity()
    {
        inputController.velocity.y += inputController.gravity * Time.deltaTime;
    }


    public override void PhysicsUpdate()
    {
        
    }

    public override void Exit()
    {
        
    }
}
