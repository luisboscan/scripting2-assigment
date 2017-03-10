using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawningActionStateBehaviour : StateBehaviour<ActionStates>
{
    public Transform checkpoint;
    public GameObject player;
    public ActionStateMachine stateMachine;
    public MovementStateMachine movementStateMachine;
    public CameraStateMachine cameraStateMachine;
    public float respawnTime;

    private float respawnStartTime;
    private Vector3 startingPosition;

    public override ActionStates GetState()
    {
        return ActionStates.Respawning;
    }

    public override StateMachine<ActionStates> GetStateMachine()
    {
        return stateMachine;
    }

    public override void EnterState()
    {
        movementStateMachine.ChangeState(MovementStates.Frozen);
        cameraStateMachine.ChangeState(CameraStates.PreIdle);
        respawnStartTime = Time.time;
        startingPosition = player.transform.position;
    }

    public override void FixedUpdateState()
    {
        float t = (Time.time - respawnStartTime) / respawnTime;
        Vector3 nextPosition = Vector3.Lerp(startingPosition, checkpoint.transform.position, t);
        player.transform.position = nextPosition;
        if (t >= 1)
        {
            stateMachine.ChangeState(ActionStates.Idle);
        }
    }
}
