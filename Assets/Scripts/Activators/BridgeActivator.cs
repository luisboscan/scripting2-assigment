using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeActivator : MonoBehaviour {

    [Tooltip("Bridge that will be moved.")]
    public GameObject bridge;
    [Tooltip("Position to move the bridge to.")]
    public Transform desiredPosition;
    [Tooltip("Speed to move at.")]
    public float speed;

    private bool active;

    void FixedUpdate()
    {
        if (active)
        {
            Vector3 nextPosition = Vector3.MoveTowards(bridge.transform.position, desiredPosition.transform.position, speed * Time.fixedDeltaTime);
            bridge.transform.Translate(nextPosition - bridge.transform.position);
        }
        active = false;
    }

    void OnTriggerStay(Collider collider)
    {
        // Move bridge as long as the trigger is activated
        active = true;
    }
}
