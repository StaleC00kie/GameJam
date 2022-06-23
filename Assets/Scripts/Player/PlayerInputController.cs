using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;
using Cinemachine;
using Utilities;
using UnityEngine.Events;

public class PlayerInputController : NetworkBehaviour
{
    [Header("Inputs")]
    public InputActionReference moveAction;
    public InputActionReference lookAction;
    public InputActionReference jetpackUpAction;
    public InputActionReference thrustDownAction;

    public float mouseSensitivity;

    private PlayerStateMachine stateMachine;

    [Header("Events")]
    public UnityEvent OnMoveActivate;
    public UnityEvent OnMoveCancel;

    public UnityEvent OnJetpackActivate;
    public UnityEvent OnJetpackCanceled;

    [Header("States")]
    [SerializeField]
    private State defaultState;

    [Header("Components")]
    [SerializeField]
    private Camera cam;

    public CinemachineVirtualCamera virtualCamera;


    [Header("Values")]
    public float gravity;

    [HideInInspector]
    public Vector2 move;

    [HideInInspector]
    public Vector3 velocity;

    [HideInInspector]
    public Vector2 mouse;

    [HideInInspector]
    public float xRot;

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

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
