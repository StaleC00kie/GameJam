using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerStateManager : NetworkBehaviour
{
    private PlayerBaseState currentState;
    private PlayerController playerController;

    public PlayerMoveState playerMoveState;

    public void Awake()
    {
        if(!isLocalPlayer)
        {
            return;
        }


        playerMoveState = GetComponent<PlayerMoveState>();

    }

    private void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        playerController = GetComponent<PlayerController>();

        // Default state
        currentState = playerMoveState;
        currentState.SetupInputs();

        currentState.Enter(playerController);
    }

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        currentState.LogicUpdate(playerController);
    }

    private void LateUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        currentState.DelayedUpdate(playerController);
    }

    public void ChangeState(PlayerBaseState state)
    {
        if (!isLocalPlayer)
        {
            return;
        }

        currentState.Exit(playerController);
        currentState = state;
        currentState.Enter(playerController);
    }
}
