using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenMovementStateBehaviour : StateBehaviour<MovementStates>
{
    public MovementStateMachine stateMachine;

    public override MovementStates GetState()
    {
        return MovementStates.Frozen;
    }

    public override StateMachine<MovementStates> GetStateMachine()
    {
        return stateMachine;
    }

    public override void EnterState()
    {
        stateMachine.characterMovement.SetSpeed(0, 0, 0);
    }
}
