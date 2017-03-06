using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStateMachine : StateMachine<CameraStates> {

    void Start()
    {
        currentState = CameraStates.Idle;
        Initialize();
    }
}
