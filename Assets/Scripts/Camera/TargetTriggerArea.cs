using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTriggerArea : MonoBehaviour
{
    public Camera camera;
    private List<GameObject> targetsInRange = new List<GameObject>();
    private List<GameObject> targetsInSight = new List<GameObject>();

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
            if (IsTargetInCameraViewport(target) && IsTargetInCameraLineOfSight(target))
            {
                targetsInSight.Add(target);
            }
        }
    }

    private bool IsTargetInCameraViewport(GameObject target)
    {
        Vector3 viewportPoint = camera.WorldToViewportPoint(target.transform.position);
        return viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1;
    }

    private bool IsTargetInCameraLineOfSight(GameObject target)
    {
        Ray ray = new Ray(camera.transform.position, target.transform.position - camera.transform.position);
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
            Vector3 viewportPoint = camera.WorldToViewportPoint(target.transform.position);
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