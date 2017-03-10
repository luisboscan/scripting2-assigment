using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovementAI : MonoBehaviour {

    [Tooltip("How much distance to move before changing direction.")]
    public float distance;
    [Tooltip("Speed to move at.")]
    public float speed;

    private float movedDistance;
    private float direction = 1;
	
	void FixedUpdate () {
        float distanceToMove = speed * Time.fixedDeltaTime;
        movedDistance += distanceToMove;
        if (movedDistance > distance)
        {
            // reverse direction when the distance 
            // has been traversed and start over
            direction *= -1;
            movedDistance = 0;
        }
        transform.Translate(transform.right * distanceToMove * direction);
	}
}
