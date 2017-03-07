using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleCameraStateBehaviour : StateBehaviour<CameraStates>
{
    public CameraStateMachine stateMachine;
    public ThirdPersonCameraController thirdPersonCameraController;
    public TargetingCameraController targetingCameraController;
    public TargetTriggerArea targetTriggerArea;
    public GameObject sprite;

    public override CameraStates GetState()
    {
        return CameraStates.Idle;
    }

    public override StateMachine<CameraStates> GetStateMachine()
    {
        return stateMachine;
    }

    public override void EnterState()
    {
        thirdPersonCameraController.enabled = true;
    }

    public override void UpdateState()
    {
        GameObject nextAvailableTarget = targetTriggerArea.getNextAvailableTarget();
        ManageTargetSprite(nextAvailableTarget);
    }

    public override void ExitState()
    {
        thirdPersonCameraController.enabled = false;
    }

    void ManageTargetSprite(GameObject target)
    {
        if (target != null)
        {
            sprite.SetActive(true);
            sprite.transform.position = target.transform.position;
            sprite.transform.LookAt(Camera.main.transform.position, -Vector3.up);
        }
        else
        {
            sprite.SetActive(false);
        }
    }
}
