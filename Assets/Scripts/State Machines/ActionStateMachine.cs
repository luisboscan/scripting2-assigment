using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

public class ActionStateMachine : MonoBehaviour {

    public PlayerInput playerInput;
    public GroundCheck groundCheck;
    public CharacterMovement characterMovement;

    // State machines
    private StateMachine<ActionStates> actionStateMachine;
    private StateMachine<MovementStates> movementStateMachine;

    // Use this for initialization
    void Start() {
        movementStateMachine = GetComponent<MovementStateMachine>().StateMachine;
        actionStateMachine = StateMachine<ActionStates>.Initialize(this, ActionStates.Idle);
    }

    void Idle_Enter()
    {
        // In idle the player can move and aim freely
        movementStateMachine.ChangeState(MovementStates.InputEnabled);
    }

    void Idle_Update()
    {
        // In idle the player can do every action
        if (playerInput.jumped)
        {
            actionStateMachine.ChangeState(ActionStates.Jumping);
        }
    }

    void Jumping_Enter()
    {
        // Let player jump in midair if he's floating
        bool canJumpInMidAir = false;
        bool jumped = characterMovement.Jump(canJumpInMidAir);
        actionStateMachine.ChangeState(ActionStates.Idle);
    }
}
