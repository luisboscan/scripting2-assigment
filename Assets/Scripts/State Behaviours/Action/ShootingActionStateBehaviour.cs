using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingActionStateBehaviour : StateBehaviour<ActionStates>
{
    public PlayerInput playerInput;
    public ActionStateMachine stateMachine;
    public MovementStateMachine movementStateMachine;
    public CameraStateMachine cameraStateMachine;
    public GatlingGun gatlingGun;
    [Tooltip("Object from where the bullets will spawn. Recomended an empty object with no collisions.")]
    public GameObject emitor;

    public override ActionStates GetState()
    {
        return ActionStates.Shooting;
    }

    public override StateMachine<ActionStates> GetStateMachine()
    {
        return stateMachine;
    }

    public override void EnterState()
    {
        movementStateMachine.ChangeState(MovementStates.InputEnabled);
    }

    public override void UpdateState()
    {
        if (playerInput.jumped)
        {
            stateMachine.ChangeState(ActionStates.Jumping);
        }
        if (playerInput.targeted)
        {
            cameraStateMachine.ToggleTargeting();
        }
        if (playerInput.teleported)
        {
            stateMachine.ChangeState(ActionStates.Teleporting);
        }
        if (!playerInput.shooting)
        {
            stateMachine.ChangeState(ActionStates.Idle);
        }
    }

    public override void FixedUpdateState()
    {
        GameObject target = cameraStateMachine.TargetedObject;
        Vector3 direction = emitor.transform.forward;
        if (target != null)
        {
            direction = target.transform.position - emitor.transform.position;
        }
        gatlingGun.Fire(emitor.transform.position, direction.normalized);
    }
}
