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
        if(inputController.moveAction.action.ReadValue<Vector2>() != Vector2.zero)
        {
            stateMachine.ChangeState(moveState);
        }
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
