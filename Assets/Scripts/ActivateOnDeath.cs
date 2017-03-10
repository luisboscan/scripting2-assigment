using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnDeath : MonoBehaviour {

    public BaseActivator activator;

    void GameObjectDied() {
        activator.Activate(gameObject);
    }
}
