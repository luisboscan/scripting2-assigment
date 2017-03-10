using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDistance : MonoBehaviour {

    [Tooltip("Distance to move.")]
    public float distance;
    [Tooltip("Speed to move at.")]
    public float speed;
    [Tooltip("Direction to move to.")]
    public Vector3 direction;

    /// <summary>
    /// Keeps track of the distance moved
    /// </summary>
    private float movedDistance;

    void FixedUpdate()
    {
        // Only move if the distance traversed is less than the distance to move
        if (movedDistance < distance)
        {
            float distanceToMove = speed * Time.fixedDeltaTime;
            movedDistance += distanceToMove;
            transform.position += distanceToMove * direction;
        }
    }
}
