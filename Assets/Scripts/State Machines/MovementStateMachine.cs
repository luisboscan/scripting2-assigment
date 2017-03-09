using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateMachine : StateMachine<MovementStates>
{
    public CharacterMovement characterMovement;

    void Start()
    {
        currentState = MovementStates.InputEnabled;
        Initialize();
    }
}
