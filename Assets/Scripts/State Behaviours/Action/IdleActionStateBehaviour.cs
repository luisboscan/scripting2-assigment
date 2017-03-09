using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleActionStateBehaviour : StateBehaviour<ActionStates>
{
    public PlayerInput playerInput;
    public ActionStateMachine stateMachine;
    public MovementStateMachine movementStateMachine;
    public CameraStateMachine cameraStateMachine;

    public override ActionStates GetState()
    {
        return ActionStates.Idle;
    }

    public override StateMachine<ActionStates> GetStateMachine()
    {
        return stateMachine;
    }

    public override void EnterState()
    {
        movementStateMachine.ChangeState(MovementStates.InputEnabled);
    }

    public override void UpdateState()
    {
        if (playerInput.jumped)
        {
            stateMachine.ChangeState(ActionStates.Jumping);
        }
        if (playerInput.targeted)
        {
            cameraStateMachine.ToggleTargeting();
        }
    }
}
