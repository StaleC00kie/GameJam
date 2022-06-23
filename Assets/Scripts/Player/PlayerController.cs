using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Cinemachine;

public class PlayerController : NetworkBehaviour
{
    [HideInInspector]
    public Vector2 move;

    [HideInInspector]
    public Vector2 mouse;

    [HideInInspector]
    public Vector3 velocity;

    [HideInInspector]
    public float xRot;

    [HideInInspector]
    public float yRot;

    [HideInInspector]
    public CharacterController cc;

    [HideInInspector]
    public PlayerControls playerControls;

    [HideInInspector]
    public Transform orientation;

    public CinemachineVirtualCamera virtualCamera;
    public Camera playerCamera;

    public float gravity;

    public float mouseSensitivity;


    public override void OnStartLocalPlayer()
    {
        playerControls = new PlayerControls();

        playerControls.Enable();

        cc = GetComponent<CharacterController>();

        virtualCamera.gameObject.SetActive(true);
        playerCamera.gameObject.SetActive(true);

        virtualCamera.transform.parent = null;
        playerCamera.transform.parent = null;

        Cursor.lockState = CursorLockMode.Locked;

    }

    public override void OnStopLocalPlayer()
    {
        playerControls.Disable();
    }


    public void Start()
    {
        if(!isLocalPlayer)
        {
            return;
        }

    }

    public void OnEnable()
    {

    }

    public void OnDisable()
    {
        if (!isLocalPlayer)
        {
            return;
        }


    }
}
