using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackState : PlayerState
{
    public float moveSpeed;

    public float jetpackForce;

    private bool held = false;
    public override void Enter()
    {

    }

    public override void LogicUpdate()
    {
        CheckStateChange();
        if (controller.isGrounded && inputController.velocity.y < 0)
        {
            inputController.velocity.y = -2.0f;
        }

        Move();
        Jetpack();
        Gravity();

        controller.Move(inputController.velocity * Time.deltaTime);
    }

    public void Move()
    {
        inputController.move = inputController.moveAction.action.ReadValue<Vector2>();

        float xMove = inputController.move.x * moveSpeed;
        float zMove = inputController.move.y * moveSpeed;

        float temp = inputController.velocity.y;

        inputController.velocity = (transform.right * xMove) + (transform.forward * zMove);

        inputController.velocity.y = temp;
    }

    public void Jetpack()
    {

        if (held)
        {
            inputController.velocity.y += jetpackForce;
        }

        //controller.Move(new Vector3(0, inputController.velocity.y, 0));
    }

    public void Gravity()
    {
        inputController.velocity.y += inputController.gravity * Time.deltaTime;
    }

    public void CheckStateChange()
    {
        if(controller.isGrounded && inputController.move == Vector2.zero)
        {
            stateMachine.ChangeState(idleState);
        }
        else if (controller.isGrounded && inputController.move != Vector2.zero)
        {
            stateMachine.ChangeState(moveState);
        }
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

    public override void PhysicsUpdate()
    {

    }

    public override void Exit()
    {

    }
}
