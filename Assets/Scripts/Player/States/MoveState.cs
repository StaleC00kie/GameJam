using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerState))]
public class MoveState : PlayerState
{
    public float moveSpeed;
    public override void Enter()
    {

    }

    public override void LogicUpdate()
    {
        Move();
    }

    public void Move()
    {
        move = inputController.moveAction.action.ReadValue<Vector2>();

        velocity = (transform.right * move.x + transform.forward * move.y) * moveSpeed;

        controller.Move(velocity * Time.deltaTime);
    }

    public override void DelayedUpdate()
    {
        Look();
    }

    public void Look()
    {
        mouse = inputController.lookAction.action.ReadValue<Vector2>() * inputController.mouseSensitivity * Time.deltaTime;

        // Rotate camera up and down
        xRot -= mouse.y;

        xRot = Mathf.Clamp(xRot, -90f, 90f);
        inputController.virtualCamera.transform.localEulerAngles = new Vector3(xRot, 0, 0);

        // Rotate player body left and right
        transform.Rotate(Vector3.up * mouse.x);
    }

    public override void PhysicsUpdate()
    {

    }

    public override void Exit()
    {

    }
}
