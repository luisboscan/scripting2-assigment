using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsActivator : BaseActivator {

    public MoveDistance moveDistance;
    public int mandatoryActivations = 3;

    private bool activated;
    private int currentActivations;

    public override void Activate(GameObject trigger)
    {
        if (!activated) {
            currentActivations++;
            if (currentActivations >= mandatoryActivations)
            {
                activated = true;
                moveDistance.enabled = true;
            }
        }
    }
}
