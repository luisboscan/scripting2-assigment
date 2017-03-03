using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

public class CameraStateMachine : MonoBehaviour {

    public ThirdPersonCameraController thirdPersonCameraController;
    public TargetingCameraController targetingCameraController;
    public TargetTriggerArea targetTriggerArea;
    public Camera camera;
    public GameObject sprite;
    public AnimationCurve throwSpeedCurve;

    // State machines
    private StateMachine<CameraStates> cameraStateMachine;

    // Use this for initialization
    void Start() {
        cameraStateMachine = StateMachine<CameraStates>.Initialize(this, CameraStates.Idle);
    }

    void PreIdle_Update()
    {
        
    }

    void Idle_Enter()
    {
        thirdPersonCameraController.enabled = true;
    }

    void Idle_Update()
    {
        GameObject nextAvailableTarget = targetTriggerArea.getNextAvailableTarget();
        ManageTargetSprite(nextAvailableTarget);
        if (Input.GetKeyDown(KeyCode.T) && nextAvailableTarget != null)
        {
            targetingCameraController.target = nextAvailableTarget;
            cameraStateMachine.ChangeState(CameraStates.Targeting);
        }
    }

    void Idle_Exit()
    {
        thirdPersonCameraController.enabled = false;
    }

    void Targeting_Enter()
    {
        targetingCameraController.enabled = true;
    }

    void Targeting_Update()
    {
        ManageTargetSprite(targetingCameraController.target);
        if (Input.GetKeyDown(KeyCode.T) || !targetTriggerArea.Targets.Contains(targetingCameraController.target))
        {
            cameraStateMachine.ChangeState(CameraStates.Idle);
        }
    }

    void Targeting_Exit()
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
