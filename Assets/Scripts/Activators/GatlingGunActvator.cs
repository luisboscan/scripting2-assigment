using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingGunActvator : MonoBehaviour
{
    public GatlingGun gatlingGun;

    private bool activated;

    void OnTriggerEnter(Collider collider)
    {
        if (!activated)
        {
            gatlingGun.IsEnabled = true;
            activated = true;
            Destroy(gameObject);
        }
    }
}
