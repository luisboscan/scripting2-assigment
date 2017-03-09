using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBehaviour<T> : MonoBehaviour {

    public abstract T GetState();

    public abstract StateMachine<T> GetStateMachine();

    void Awake()
    {
        GetStateMachine().RegisterStateBehaviour(GetState(), this);
    }

    public virtual bool CanEnterState()
    {
        return true;
    }

    public virtual void EnterState ()
    {

    }

    public virtual void UpdateState()
    {

    }

    public virtual void FixedUpdateState()
    {

    }

    public virtual void ExitState()
    {

    }
}
