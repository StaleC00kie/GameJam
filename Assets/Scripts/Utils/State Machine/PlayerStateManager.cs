using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerStateManager : NetworkBehaviour
{
    public PlayerBaseState currentState;
    private PlayerController playerController;

    [HideInInspector]
    public PlayerMoveState playerMoveState;
    [HideInInspector]
    public PlayerJetpackState playerJetpackState;



    public void Awake()
    {
        if(!isLocalPlayer)
        {
            return;
        }
    }

    private void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        playerMoveState = GetComponent<PlayerMoveState>();
        playerJetpackState = GetComponent<PlayerJetpackState>();


        playerController = GetComponent<PlayerController>();

        // Default state
        currentState = playerMoveState;

        currentState.SetupInputs(playerController);

        currentState.Enter(playerController);
    }

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        currentState.CheckStateTransitions(this);

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

        currentState.RemoveInputs();
        currentState.Exit(playerController);
        currentState = state;
        currentState.SetupInputs(playerController);
        currentState.Enter(playerController);
    }
}
