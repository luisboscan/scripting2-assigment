using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitioningCameraStateBehaviour : StateBehaviour<CameraStates>
{
    public CameraStateMachine stateMachine;

    public AnimationCurve throwSpeedCurve;
    public CameraController destinationCameraController;
    public CameraStates nextState;

    private float startTime;
    public float transitionTime = 0.5f;

    Vector3 originPosition;
    Quaternion originRotation;

    public override CameraStates GetState()
    {
        return CameraStates.Transitioning;
    }

    public override StateMachine<CameraStates> GetStateMachine()
    {
        return stateMachine;
    }

    public override void EnterState()
    {
        startTime = Time.time;
        originPosition = transform.position;
        originRotation = transform.rotation;
        destinationCameraController.Reset();
    }

    public override void FixedUpdateState()
    {
        float elapsedTime = Time.time - startTime;
        float delta = throwSpeedCurve.Evaluate(elapsedTime / transitionTime);

        Vector3 nextPosition;
        Quaternion nextRotation;
        destinationCameraController.GetNextState(out nextPosition, out nextRotation);

        transform.position = Vector3.Lerp(originPosition, nextPosition, delta);
        transform.rotation = Quaternion.Lerp(originRotation, nextRotation, delta);

        if (delta >= 1)
        {
            stateMachine.ChangeState(nextState);
        }
    }
}
