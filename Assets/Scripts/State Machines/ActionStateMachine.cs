using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionStateMachine : StateMachine<ActionStates>
{
    void Start()
    {
        currentState = ActionStates.Idle;
        Initialize();
    }
}
