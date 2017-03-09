using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStateMachine : StateMachine<CameraStates>
{
    public ThirdPersonCameraController thirdPersonCameraController;
    public TargetingCameraController targetingCameraController;
    public TargetTriggerArea targetTriggerArea;
    public GameObject targettingSprite;

    void Start()
    {
        currentState = CameraStates.Idle;
        Cursor.lockState = CursorLockMode.Locked;
        Initialize();
    }

    public void ToggleTargeting()
    {
        if (State == CameraStates.Idle || State == CameraStates.PreIdle)
        {
            ChangeState(CameraStates.PreTargeting);
        }
        else if (State == CameraStates.Targeting || State == CameraStates.PreTargeting)
        {
            ChangeState(CameraStates.PreIdle);
        }
    }

    public void RenderTargetingSprite(GameObject target)
    {
        if (target != null)
        {
            targettingSprite.SetActive(true);
            targettingSprite.transform.position = target.transform.position;
            targettingSprite.transform.LookAt(Camera.main.transform.position, -Vector3.up);
        }
        else
        {
            targettingSprite.SetActive(false);
        }
    }
}
