using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleCameraStateBehaviour : StateBehaviour<CameraStates>
{
    public CameraStateMachine stateMachine;

    public override CameraStates GetState()
    {
        return CameraStates.Idle;
    }

    public override StateMachine<CameraStates> GetStateMachine()
    {
        return stateMachine;
    }

    public override void UpdateState()
    {
        GameObject nextAvailableTarget = stateMachine.targetTriggerArea.getNextTarget();
        stateMachine.RenderTargetingSprite(nextAvailableTarget);
    }

    public override void FixedUpdateState()
    {
        stateMachine.thirdPersonCameraController.UpdateCameraState();
    }
}
