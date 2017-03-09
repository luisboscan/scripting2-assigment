using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleCameraStateBehaviour : StateBehaviour<CameraStates>
{
    public GameObject player;
    public CameraStateMachine stateMachine;
    public MovementStateMachine movementStateMachine;
    public PlayerInput playerInput;

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

        if (movementStateMachine.State == MovementStates.InputEnabled && (playerInput.horizontalDirection != 0 || playerInput.verticalDirection != 0))
        {
            Quaternion rotation = Camera.main.transform.rotation;
            rotation = Quaternion.Euler(0, rotation.eulerAngles.y, rotation.eulerAngles.z);
            player.transform.rotation = Quaternion.LookRotation(rotation * new Vector3(playerInput.horizontalDirection, 0, playerInput.verticalDirection));
        }
    }
}
