using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnDeath : MonoBehaviour {

    public BaseActivator activator;

    void OnDestroy() {
        activator.Activate(gameObject);
    }
}
