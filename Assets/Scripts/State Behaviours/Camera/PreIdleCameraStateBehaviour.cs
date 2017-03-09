using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreIdleCameraStateBehaviour : TransitioningCameraStateBehaviour
{
    public CameraStateMachine stateMachine;
    public ThirdPersonCameraController thirdPersonCameraController;

    public override CameraStates GetState()
    {
        return CameraStates.PreIdle;
    }

    public override StateMachine<CameraStates> GetStateMachine()
    {
        return stateMachine;
    }

    public override void EnterState()
    {
        cameraTransitionObject = new CameraTransitionObject(thirdPersonCameraController, CameraStates.Idle);
        base.EnterState();
    }
}
