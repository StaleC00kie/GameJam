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
        move = inputController.moveAction.action.ReadValue<Vector2>();

        velocity = new Vector3(move.x, velocity.y, move.y) * moveSpeed;

        controller.Move(velocity * Time.deltaTime);
    }

    public override void DelayedUpdate()
    {

    }

    public override void PhysicsUpdate()
    {

    }

    public override void Exit()
    {

    }
}
