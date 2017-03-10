using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTriggerArea : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        collider.gameObject.GetComponentInChildren<ActionStateMachine>().ChangeState(ActionStates.Respawning);
    }
}
