using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEnabledMovementStateBehaviour : StateBehaviour<MovementStates>
{
    public PlayerInput playerInput;
    public MovementStateMachine stateMachine;

    public override MovementStates GetState()
    {
        return MovementStates.InputEnabled;
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
        stateMachine.characterMovement.UpdateInput(playerInput.horizontalDirection, playerInput.verticalDirection, playerInput.holdingJump);
        stateMachine.characterMovement.Move();
    }
}
