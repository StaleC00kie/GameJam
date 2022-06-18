using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    private PlayerController _cc;//



    private void Awake()
    {
        _cc = GetComponent<PlayerController>();
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            //data.direction.Normalize();
            _cc.Move(data.direction);
        }
    }

    private void LateUpdate()
    {   

    }
}
