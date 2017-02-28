using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCameraScript : MonoBehaviour {

    public GameObject player;
    public GameObject target;

    private float distanceFromPlayer = 3f;
    private float angle = 45f;
    

	// Use this for initialization
	void Start () {
	}

    Vector3 middlePoint;
    Vector3 velocity;
    // Update is called once per frame
    void FixedUpdate () {
        transform.position = Vector3.SmoothDamp(transform.position, GetNextPosition(), ref velocity, 0.3f);

        transform.LookAt(middlePoint);

        


    }

    Vector3 GetNextPosition()
    {
        Vector3 currentPosition = transform.position;
        Vector3 distance = target.transform.position - player.transform.position;
        middlePoint = target.transform.position - distance / 2;

        Vector3 cameraOffset = -distance;
        cameraOffset = Vector3.Normalize(cameraOffset);
        cameraOffset.y = Mathf.Sin(angle * Mathf.Deg2Rad);
        transform.position = player.transform.position + cameraOffset * distanceFromPlayer;
        transform.RotateAround(middlePoint, Vector3.up, 15);
        Vector3 nextPosition = transform.position;
        transform.position = currentPosition;
        return nextPosition;
    }

    Vector2 GetXYDirection(float angle, float magnitude) {
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * magnitude;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(middlePoint, 1);
    }
}
