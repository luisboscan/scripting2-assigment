using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CameraTransitionObject
{
    public CameraController controllerToTransitionTo;
    public CameraStates stateToTransitionTo;

    public CameraTransitionObject(CameraController controllerToTransitionTo, CameraStates stateToTransitionTo)
    {
        this.controllerToTransitionTo = controllerToTransitionTo;
        this.stateToTransitionTo = stateToTransitionTo;
    }
}
