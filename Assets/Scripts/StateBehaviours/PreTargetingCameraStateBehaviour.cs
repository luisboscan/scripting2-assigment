using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreTargetingCameraStateBehaviour : TransitioningCameraStateBehaviour
{
    public CameraStateMachine stateMachine;
    public TargetingCameraController targetingCameraController;
    public TargetTriggerArea targetTriggerArea;

    public override CameraStates GetState()
    {
        return CameraStates.PreTargeting;
    }

    public override StateMachine<CameraStates> GetStateMachine()
    {
        return stateMachine;
    }

    public override void EnterState()
    {
        GameObject nextAvailableTarget = targetTriggerArea.getNextAvailableTarget();
        targetingCameraController.target = nextAvailableTarget;
        cameraTransitionObject = new CameraTransitionObject(targetingCameraController, CameraStates.Targeting);
        base.EnterState();
    }

    public override bool CanEnterState()
    {
        GameObject nextAvailableTarget = targetTriggerArea.getNextAvailableTarget();
        return nextAvailableTarget != null;
    }
}
