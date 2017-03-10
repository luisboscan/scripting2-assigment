using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingCameraStateBehaviour : StateBehaviour<CameraStates>
{
    public CameraStateMachine stateMachine;
    public GameObject player;

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
        GameObject target = stateMachine.targetingCameraController.target;
        if (target == null || !stateMachine.targetTriggerArea.IsTargetInRange(target))
        {
            stateMachine.ChangeState(CameraStates.PreIdle);
        }
    }

    public override void FixedUpdateState()
    {
        stateMachine.targetingCameraController.UpdateCameraState();
        player.transform.LookAt(stateMachine.targetingCameraController.target.transform);
    }

    public override bool CanEnterState()
    {
        GameObject nextAvailableTarget = stateMachine.targetTriggerArea.getNextTarget();
        return nextAvailableTarget != null && stateMachine.State != CameraStates.Targeting;
    }
}
