using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerBaseState : NetworkBehaviour
{
    [HideInInspector]
    public PlayerControls playerControls;

    [HideInInspector]
    public PlayerController playerController;

    public void Awake()
    {
        playerControls = new PlayerControls();
    }

    public void OnEnable()
    {
        playerControls.Enable();
    }

    public void OnDisable()
    {
        playerControls.Disable();
    }

    public virtual void SetupInputs()
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
    public virtual void Exit(PlayerController _playerController)
    {

    }

    public void GetInput()
    {

    }
}
