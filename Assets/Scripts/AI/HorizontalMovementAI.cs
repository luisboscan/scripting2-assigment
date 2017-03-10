using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovementAI : MonoBehaviour {

    public float distance;
    public float speed;

    private float movedDistance;
    private float direction = 1;
	
	// Update is called once per frame
	void FixedUpdate () {
        float distanceToMove = speed * Time.deltaTime;
        movedDistance += distanceToMove;
        if (movedDistance > distance)
        {
            direction *= -1;
            movedDistance = 0;
        }
        transform.Translate(transform.right * distanceToMove * direction);
	}
}
