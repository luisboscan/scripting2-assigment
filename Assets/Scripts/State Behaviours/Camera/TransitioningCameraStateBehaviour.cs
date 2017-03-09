using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TransitioningCameraStateBehaviour : StateBehaviour<CameraStates>
{
    public Camera cameraComponent;
    public AnimationCurve transitionSpeedCurve;
    public float transitionTime = 0.3f;

    protected CameraTransitionObject cameraTransitionObject;

    private float startTime;
    private Vector3 originCameraPosition;
    private Quaternion originCameraRotation;

    public override void EnterState()
    {
        startTime = Time.time;
        originCameraPosition = cameraComponent.transform.position;
        originCameraRotation = cameraComponent.transform.rotation;
        cameraTransitionObject.controllerToTransitionTo.Reset();
    }

    public override void FixedUpdateState()
    {
        float elapsedTime = Time.time - startTime;
        float delta = transitionSpeedCurve.Evaluate(elapsedTime / transitionTime);

        Vector3 nextPosition;
        Quaternion nextRotation;
        cameraTransitionObject.controllerToTransitionTo.GetNextState(out nextPosition, out nextRotation);

        cameraComponent.transform.position = Vector3.Lerp(originCameraPosition, nextPosition, delta);
        cameraComponent.transform.rotation = Quaternion.Lerp(originCameraRotation, nextRotation, delta);

        if (delta >= 1)
        {
            GetStateMachine().ChangeState(cameraTransitionObject.stateToTransitionTo);
        }
    }
}
