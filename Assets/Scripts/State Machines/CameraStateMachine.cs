using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStateMachine : StateMachine<CameraStates> {

    void Start()
    {
        currentState = CameraStates.Idle;
        Initialize();
        Cursor.lockState = CursorLockMode.Locked;
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
}
