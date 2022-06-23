using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerState : State
{

    [HideInInspector]
    public PlayerInputController inputController;

    [HideInInspector]
    public PlayerStateMachine stateMachine;

    [HideInInspector]
    public IdleState idleState;

    [HideInInspector]
    public MoveState moveState;

    [HideInInspector]
    public JetpackState jetpackState;


    [HideInInspector]
    public CharacterController controller;

    public void Awake()
    {
        inputController = GetComponent<PlayerInputController>();
        stateMachine = GetComponent<PlayerStateMachine>();

        idleState = GetComponent<IdleState>();
        moveState = GetComponent<MoveState>();
        jetpackState = GetComponent<JetpackState>();

        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        inputController = GetComponent<PlayerInputController>();
    }
    private void OnDisable()
    {
        
    }


}
