using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeActivator : MonoBehaviour {

    public GameObject bridge;
    public Transform desiredPosition;
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
        active = true;
    }
}
