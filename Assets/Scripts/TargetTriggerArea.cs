using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTriggerArea : MonoBehaviour
{
    public Camera camera;
    private List<GameObject> targets = new List<GameObject>();
    private List<GameObject> targetsInSight = new List<GameObject>();

    public GameObject getNextAvailableTarget()
    {
        GameObject nextTarget = null;
        targetsInSight.Clear();
        for (int i = 0; i < targets.Count; i++)
        {
            GameObject target = targets[i];
            Vector3 viewportPoint = camera.WorldToViewportPoint(target.transform.position);
            bool visible = viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1;
            if (!visible)
            {
                continue;
            }
            Ray ray = new Ray(camera.transform.position, target.transform.position - camera.transform.position);
            RaycastHit raycastHit;
            bool hit = Physics.Raycast(ray, out raycastHit);
            if (!hit || raycastHit.collider.gameObject != target)
            {
                continue;
            }
            targetsInSight.Add(target);
        }
        float minDistance = 0;
        for (int i = 0; i < targetsInSight.Count; i++)
        {
            GameObject target = targetsInSight[i];
            Vector3 viewportPoint = camera.WorldToViewportPoint(target.transform.position);
            float distanceX = Mathf.Abs(viewportPoint.x - 0.5f);
            if (i == 0 || distanceX < minDistance)
            {
                nextTarget = target;
                minDistance = distanceX;
            }
        }
        Debug.Log("Count: " + targetsInSight.Count);
        return nextTarget;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInChildren<Targetable>() == null)
        {
            return;
        }
        targets.Add(other.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        targets.Remove(other.gameObject);
    }

    public void RemoveSoulFromArea(GameObject soul)
    {
        targets.Remove(soul);
    }

    public List<GameObject> Targets
    {
        get { return targets; }
    }
}