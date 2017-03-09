using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDisabledMovementStateBehaviour : StateBehaviour<MovementStates>
{
    public MovementStateMachine stateMachine;

    public override MovementStates GetState()
    {
        return MovementStates.InputDisabled;
    }

    public override StateMachine<MovementStates> GetStateMachine()
    {
        return stateMachine;
    }

    public override void EnterState()
    {
        stateMachine.characterMovement.UpdateInput(0, 0, false);
    }

    public override void FixedUpdateState()
    {
        stateMachine.characterMovement.Move();
    }
}
