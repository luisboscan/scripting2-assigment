using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportingActionStateBehaviour : StateBehaviour<ActionStates>
{
    public Teleport teleport;
    public ActionStateMachine stateMachine;
    public MovementStateMachine movementStateMachine;
    public CameraStateMachine cameraStateMachine;
    public TargetTriggerArea targetTriggerArea;

    public override ActionStates GetState()
    {
        return ActionStates.Teleporting;
    }

    public override StateMachine<ActionStates> GetStateMachine()
    {
        return stateMachine;
    }

    public override bool CanEnterState()
    {
        GameObject nextTarget = targetTriggerArea.getNextTarget();
        return nextTarget != null && nextTarget.GetComponent<Teleportable>() != null;
    }

    public override void EnterState()
    {
        teleport.BeginTeleport(targetTriggerArea.getNextTarget());
        movementStateMachine.ChangeState(MovementStates.Frozen);
        cameraStateMachine.ChangeState(CameraStates.PreIdle);
    }

    public override void FixedUpdateState()
    {
        bool finished = teleport.UpdateTeleport();
        if (finished)
        {
            stateMachine.ChangeState(ActionStates.Idle);
        }
    }
}
