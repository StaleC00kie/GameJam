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
    public CharacterController cc;

    public CinemachineVirtualCamera virtualCamera;
    public Camera playerCamera;

    public float gravity;

    public float mouseSensitivity;

    public void Start()
    {
        if(!isLocalPlayer)
        {
            return;
        }

        cc = GetComponent<CharacterController>();

        virtualCamera.gameObject.SetActive(true);
        playerCamera.gameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
    }
}
