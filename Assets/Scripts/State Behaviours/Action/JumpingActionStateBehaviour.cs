using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingActionStateBehaviour : StateBehaviour<ActionStates>
{
    public CharacterMovement characterMovement;
    public ActionStateMachine stateMachine;

    public override ActionStates GetState()
    {
        return ActionStates.Jumping;
    }

    public override StateMachine<ActionStates> GetStateMachine()
    {
        return stateMachine;
    }

    public override void EnterState()
    {
        bool canJumpInMidAir = false;
        characterMovement.Jump(canJumpInMidAir);
        stateMachine.ChangeState(ActionStates.Idle);
    }
}
