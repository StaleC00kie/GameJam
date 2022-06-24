using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerBaseState : NetworkBehaviour
{
    [HideInInspector]
    public PlayerController playerController;

    public void Start()
    {
        if(!isLocalPlayer)
        {
            return;
        }


    }

    public virtual void SetupInputs(PlayerController _playerController)
    {

    }

    public virtual void RemoveInputs()
    {

    }

    public virtual void Enter(PlayerController _playerController)
    {

    }

    public virtual void HandleInput(PlayerController _playerController)
    {

    }

    public virtual void LogicUpdate(PlayerController _playerController)
    {

    }
    public virtual void DelayedUpdate(PlayerController _playerController)
    {

    }
    public virtual void PhysicsUpdate(PlayerController _playerController)
    {

    }

    public virtual void CheckStateTransitions(PlayerStateManager stateMachine)
    {

    }
    public virtual void Exit(PlayerController _playerController)
    {

    }
}
