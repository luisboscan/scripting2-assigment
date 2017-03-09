using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Hazard
{
    public float lifeSpan = 2;
    public float speed = 5;
    [Tooltip("Layers that will be ignored by the bullet's collision.")]
    public LayerMask ignoreCollisionMask;
    private Vector3 direction;
    private Vector3 tmp;

    void Update()
    {
        // Set desired travel speed
        tmp = direction * speed * Time.deltaTime;
        // Do a raycast to see if anything will be hit between the current and the next position
        int collidableLayer = ~ignoreCollisionMask.value;
        RaycastHit hitInfo;
        bool hit = Physics.Raycast(transform.position, tmp, out hitInfo, tmp.magnitude, collidableLayer);
        if (hit)
        {
            // If there's a hit, reduce the travel distance to match the position of the hit object
            // This is done to avoid traversing objects that are too close to the bullet
            tmp = Vector3.ClampMagnitude(tmp, hitInfo.distance);
        }
        transform.Translate(tmp, Space.World);
    }

    void OnEnable()
    {
        Invoke("Destroy", lifeSpan);
    }

    void Destroy()
    {
        BulletPool.instance.ReleaseObject(gameObject);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    // bullet hit something, do damage and destroy!
    public void OnTriggerEnter(Collider other)
    {
        // If the bullet hits something that it's supposed to be ignored, don't do anything
        if (Util.IsObjectInLayerMask(ignoreCollisionMask, other.gameObject)) return;
        DoDamage(other.gameObject);
        Destroy();
    }

    // has to be on the object that has the renderer component!
    public void OnBecameInvisible()
    {
        Destroy();
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }
}
