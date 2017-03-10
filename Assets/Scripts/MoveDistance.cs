using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDistance : MonoBehaviour {

    public float distance;
    public float speed;
    public Vector3 direction;

    private float movedDistance;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movedDistance < distance)
        {
            float distanceToMove = speed * Time.fixedDeltaTime;
            movedDistance += distanceToMove;
            transform.position += distanceToMove * direction;
        }
    }
}
