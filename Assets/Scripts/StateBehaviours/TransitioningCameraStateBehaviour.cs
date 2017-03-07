using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TransitioningCameraStateBehaviour : StateBehaviour<CameraStates>
{
    protected CameraTransitionObject cameraTransitionObject;
    public AnimationCurve throwSpeedCurve;

    private float startTime;
    public float transitionTime = 0.5f;

    Vector3 originPosition;
    Quaternion originRotation;

    public override void EnterState()
    {
        startTime = Time.time;
        originPosition = transform.position;
        originRotation = transform.rotation;
        cameraTransitionObject.ControllerToTransitionTo.Reset();
    }

    public override void FixedUpdateState()
    {
        float elapsedTime = Time.time - startTime;
        float delta = throwSpeedCurve.Evaluate(elapsedTime / transitionTime);

        Vector3 nextPosition;
        Quaternion nextRotation;
        cameraTransitionObject.ControllerToTransitionTo.GetNextState(out nextPosition, out nextRotation);

        transform.position = Vector3.Lerp(originPosition, nextPosition, delta);
        transform.rotation = Quaternion.Lerp(originRotation, nextRotation, delta);

        if (delta >= 1)
        {
            GetStateMachine().ChangeState(cameraTransitionObject.StateToTransitionTo);
        }
    }
}
