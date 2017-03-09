using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingCameraStateBehaviour : StateBehaviour<CameraStates>
{
    public CameraStateMachine stateMachine;

    public override CameraStates GetState()
    {
        return CameraStates.Targeting;
    }

    public override StateMachine<CameraStates> GetStateMachine()
    {
        return stateMachine;
    }

    public override void UpdateState()
    {
        stateMachine.RenderTargetingSprite(stateMachine.targetingCameraController.target);
        if (!stateMachine.targetTriggerArea.IsTargetInRange(stateMachine.targetingCameraController.target))
        {
            stateMachine.ChangeState(CameraStates.PreIdle);
        }
    }

    public override void FixedUpdateState()
    {
        stateMachine.targetingCameraController.UpdateCameraState();
    }

    public override bool CanEnterState()
    {
        GameObject nextAvailableTarget = stateMachine.targetTriggerArea.getNextTarget();
        return nextAvailableTarget != null;
    }
}
