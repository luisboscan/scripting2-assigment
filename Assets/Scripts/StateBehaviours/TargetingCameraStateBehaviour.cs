using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingCameraStateBehaviour : StateBehaviour<CameraStates>
{
    public CameraStateMachine stateMachine;

    public TargetingCameraController targetingCameraController;
    public TargetTriggerArea targetTriggerArea;
    public GameObject sprite;

    public override CameraStates GetState()
    {
        return CameraStates.Targeting;
    }

    public override StateMachine<CameraStates> GetStateMachine()
    {
        return stateMachine;
    }

    public override void EnterState()
    {
        targetingCameraController.enabled = true;
    }

    public override void FixedUpdateState()
    {
        ManageTargetSprite(targetingCameraController.target);
        if (Input.GetKeyDown(KeyCode.T) || !targetTriggerArea.Targets.Contains(targetingCameraController.target))
        {
            stateMachine.ChangeState(CameraStates.Idle);
        }
    }

    public override void ExitState()
    {
        targetingCameraController.enabled = false;
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
