using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Utilities
{
    public class StateMachine : NetworkBehaviour
    {
        [HideInInspector]
        public State currentState { get; private set; }

        public virtual void Init(State defaultState)
        {
            currentState = defaultState;
        }

        public virtual void ChangeState(State newState)
        {
            currentState.Exit();

            currentState = newState;

            currentState.Enter();
        }

        public void Update()
        {
            if(!isLocalPlayer)
            {
                return;
            }

            if (currentState == null)
            {
                return;
            }

            currentState.LogicUpdate();
        }

        public void FixedUpdate()
        {
            if (!isLocalPlayer)
            {
                return;
            }

            if (currentState == null)
            {
                return;
            }

            currentState.PhysicsUpdate();
        }

        public void LateUpdate()
        {
            if (!isLocalPlayer)
            {
                return;
            }

            if(currentState == null)
            {
                return;
            }
            currentState.DelayedUpdate();
        }
    }

}
