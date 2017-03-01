using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTriggerArea : MonoBehaviour
{
    public Camera camera;
    public float raycastLength;
    private List<GameObject> targets = new List<GameObject>();
    private List<GameObject> targetsInSight = new List<GameObject>();
    public Canvas canvas;
    public GameObject sprite;
    public TargetCameraScript targetCameraScript;

    private GameObject currentTarget;

    void Update()
    {
        targetsInSight.Clear();
        sprite.SetActive(false);
        currentTarget = null;
        for (int i=0; i<targets.Count; i++)
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
            bool hit = Physics.Raycast(ray, out raycastHit, raycastLength);
            if (!hit || raycastHit.collider.gameObject != target)
            {
                continue;
            }
            targetsInSight.Add(target);
        }
        float minDistance = 0;
        for (int i=0; i<targetsInSight.Count; i++)
        {
            GameObject target = targetsInSight[i];
            Vector3 viewportPoint = camera.WorldToViewportPoint(target.transform.position);
            float distanceX = Mathf.Abs(viewportPoint.x - 0.5f);
            if (currentTarget == null || distanceX < minDistance)
            {
                currentTarget = target;
                minDistance = distanceX;
            }
        }
        if (currentTarget != null)
        {
            sprite.SetActive(true);
            sprite.transform.position = currentTarget.transform.position;
            sprite.transform.LookAt(Camera.main.transform.position, -Vector3.up);
        }
        if (currentTarget != null && Input.GetKeyDown(KeyCode.T))
        {
            targetCameraScript.enabled = true;
        }
        if (currentTarget == null)
        {
            targetCameraScript.enabled = false;
        }
        Debug.Log("Count: " + targetsInSight.Count);
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