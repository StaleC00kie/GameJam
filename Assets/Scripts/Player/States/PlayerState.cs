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
    public Vector2 move;

    [HideInInspector]
    public Vector3 velocity;

    [HideInInspector]
    public Vector2 mouse;

    [HideInInspector]
    public float xRot;

    [HideInInspector]
    public CharacterController controller;
    //public override void Enter()
    //{
    //    base.Enter();
    //}

    //public override void LogicUpdate()
    //{
    //    base.LogicUpdate();
    //}

    //public override void DelayedUpdate()
    //{
    //    base.DelayedUpdate();
    //}

    //public override void PhysicsUpdate()
    //{
    //    base.PhysicsUpdate();
    //}

    //public override void Exit()
    //{
    //    base.Exit();
    //}

    public void Awake()
    {
        inputController = GetComponent<PlayerInputController>();
        stateMachine = GetComponent<PlayerStateMachine>();

        idleState = GetComponent<IdleState>();
        moveState = GetComponent<MoveState>();

        controller = GetComponent<CharacterController>();
    }


}
