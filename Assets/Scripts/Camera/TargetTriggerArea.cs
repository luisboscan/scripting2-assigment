using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTriggerArea : MonoBehaviour
{
    public Camera cameraComponent;

    private List<GameObject> targetsInRange = new List<GameObject>();
    private List<GameObject> targetsInSight = new List<GameObject>();

    private void Update()
    {
        // Cleanup targets from the list that have been destroyed
        for(int i = targetsInRange.Count-1; i>=0; i--)
        {
            if (targetsInRange[i] == null)
            {
                targetsInRange.RemoveAt(i);
            }
        }
    }

    public GameObject getNextTarget()
    {
        FilterVisibleTargets(targetsInRange, ref targetsInSight);
        GameObject nextTarget = GetTargetClosestToCameraCenter(targetsInSight);
        return nextTarget;
    }

    public bool IsTargetInRange(GameObject target)
    {
        return targetsInRange.Contains(target);
    }

    private void FilterVisibleTargets(List<GameObject> targets, ref List<GameObject> targetsInSight)
    {
        targetsInSight.Clear();
        for (int i = 0; i < targets.Count; i++)
        {
            GameObject target = targets[i];
            if (target != null && IsTargetInCameraViewport(target) && IsTargetInCameraLineOfSight(target))
            {
                targetsInSight.Add(target);
            }
        }
    }

    /// <summary>
    /// Checks if the target is currently in the camera viewport
    /// </summary>
    private bool IsTargetInCameraViewport(GameObject target)
    {
        Vector3 viewportPoint = cameraComponent.WorldToViewportPoint(target.transform.position);
        return viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1;
    }

    /// <summary>
    /// Checks if the target is being blocked by another object when being seen by the camera
    /// </summary>
    private bool IsTargetInCameraLineOfSight(GameObject target)
    {
        Ray ray = new Ray(cameraComponent.transform.position, target.transform.position - cameraComponent.transform.position);
        RaycastHit raycastHit;
        bool hit = Physics.Raycast(ray, out raycastHit);
        return hit && raycastHit.collider.gameObject == target;
    }

    private GameObject GetTargetClosestToCameraCenter(List<GameObject> targets)
    {
        float minDistance = 0;
        GameObject closestTarget = null;
        for (int i = 0; i < targets.Count; i++)
        {
            GameObject target = targets[i];
            // Choose the target that is closest to the camera center in X
            Vector3 viewportPoint = cameraComponent.WorldToViewportPoint(target.transform.position);
            float distanceX = Mathf.Abs(viewportPoint.x - 0.5f);
            if (i == 0 || distanceX < minDistance)
            {
                closestTarget = target;
                minDistance = distanceX;
            }
        }
        return closestTarget;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Targetable>() == null)
        {
            return;
        }
        targetsInRange.Add(other.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        targetsInRange.Remove(other.gameObject);
    }
}