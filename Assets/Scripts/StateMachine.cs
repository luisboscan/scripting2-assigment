using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> : MonoBehaviour {

    protected T currentState;
    protected Dictionary<T, StateBehaviour<T>> stateBehaviours = new Dictionary<T, StateBehaviour<T>>();
    private bool initialized;

    protected void Initialize()
    {
        initialized = true;
        stateBehaviours[currentState].EnterState();
    }

    void Update ()
    {
        if (initialized)
        {
            stateBehaviours[currentState].UpdateState();
        }
    }

    void FixedUpdate()
    {
        if (initialized)
        {
            stateBehaviours[currentState].FixedUpdateState();
        }
    }

    public bool ChangeState(T nextState)
    {
        StateBehaviour<T> currentStateBehaviour = stateBehaviours[currentState];
        StateBehaviour<T> nextStateBehaviour = stateBehaviours[nextState];
        if (nextStateBehaviour.CanEnterState())
        {
            currentState = nextState;
            if (initialized)
            {
                currentStateBehaviour.ExitState();
                nextStateBehaviour.EnterState();
            }
            return true;
        }
        return false;
    }

    public void RegisterStateBehaviour(T state, StateBehaviour<T> stateBehaviour)
    {
        stateBehaviours.Add(state, stateBehaviour);
    }

    public StateBehaviour<T> GetStateBehaviour(T state)
    {
        return stateBehaviours[state];
    }

    public T State
    {
        get { return currentState; }
    }
}
