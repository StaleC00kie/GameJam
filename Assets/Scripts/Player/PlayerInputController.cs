using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;
using Cinemachine;
using Utilities;

public class PlayerInputController : NetworkBehaviour
{
    public InputActionReference moveAction;
    public InputActionReference lookAction;
    public InputActionReference thrustUpAction;
    public InputActionReference thrustDownAction;

    private PlayerStateMachine stateMachine;

    [SerializeField]
    private State defaultState;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        if (!isLocalPlayer)
        {
            return;
        }

    }

    public void Start()
    {
        if(!isLocalPlayer)
        {
            return;
        }


        stateMachine = GetComponent<PlayerStateMachine>();
        cam.gameObject.SetActive(true);
        virtualCamera.gameObject.SetActive(true);

        stateMachine.Init(defaultState);
    }
}
