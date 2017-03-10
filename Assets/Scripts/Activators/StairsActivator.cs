using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsActivator : BaseActivator {

    [Tooltip("Script that will be activated once all the activations are done.")]
    public MoveDistance moveDistance;
    [Tooltip("How many activations are necesary before activating the stairs.")]
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
