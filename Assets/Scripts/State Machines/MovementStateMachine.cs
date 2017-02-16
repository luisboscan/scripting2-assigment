using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

public class MovementStateMachine : MonoBehaviour {

    public PlayerInput playerInput;
    public CharacterMovement characterMovement;
    public GroundCheck groundCheck;
    private StateMachine<MovementStates> stateMachine;    

    // Use this for initialization
    void Awake() {
        stateMachine = StateMachine<MovementStates>.Initialize(this, MovementStates.InputEnabled);
    }

    void Frozen_Enter()
    {
        characterMovement.SetSpeed(0, 0, 0);
    }

    void InputDisabled_Enter()
    {
        characterMovement.UpdateInput(0, 0, false);
    }

    void InputDisabled_FixedUpdate()
    {
        characterMovement.Move();
    }

    void InputEnabled_FixedUpdate()
    {
        characterMovement.UpdateInput(playerInput.horizontalDirection, playerInput.verticalDirection, playerInput.holdingJump);
        characterMovement.Move();
    }

    public StateMachine<MovementStates> StateMachine
    {
        get { return stateMachine; }
    }
}
