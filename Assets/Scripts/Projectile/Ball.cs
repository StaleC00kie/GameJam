using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Ball : NetworkBehaviour
{
    public float turnSpeed;
    public float speed;

    private Rigidbody ballBody;

    private Transform targetPlayer;

    private NetworkManager networkManager;




    private void Awake()
    {

    }
    public void Start()
    {

    }

    public void FixedUpdate()
    {


    }

    [Server]
    public void MoveBall()
    {

    }

}
