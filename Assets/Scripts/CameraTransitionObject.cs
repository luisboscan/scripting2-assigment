using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransitionObject
{
    private CameraController controllerToTransitionTo;
    private CameraStates stateToTransitionTo;

    public CameraTransitionObject(CameraController controllerToTransitionTo, CameraStates stateToTransitionTo)
    {
        this.controllerToTransitionTo = controllerToTransitionTo;
        this.stateToTransitionTo = stateToTransitionTo;
    }

    public CameraStates StateToTransitionTo
    {
        get { return stateToTransitionTo; }
    }

    public CameraController ControllerToTransitionTo
    {
        get { return controllerToTransitionTo; }
    }
}
