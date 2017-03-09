using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GatlingGun : MonoBehaviour
{
    [Tooltip("Offset in degrees used to randomize the direction of the bullet. Ex: if the bullet is going right and the value is 5, the angle will be randomized in a range of -5 to 5.")]
    public float bulletAngleRandomOffset = 1.5f;
    [Tooltip("Offset in coordinates used to randomize the spawn point of the bullet.")]
    public float bulletSpawnPositionRandomOffset = 0.15f;
    [Tooltip("Delay between bullets.")]
    public float fireRate = 0.05f;
    [Tooltip("Optional bullet speed, this will override the speed on the bullet prefab if overrideBulletSpeed is set to true.")]
    public float bulletSpeed = 15f;
    [Tooltip("If true it will override the bullet's prefab speed with the one on this component.")]
    public bool overrideBulletSpeed = true;
    [Tooltip("Multiplier for how fast the overheat should recover when not shooting. Ex: if overheatLimit is 6s, a value of 2 will make it recover in 3s.")]
    public float recoverRate = 2;
    public bool isEnabled = true;
    [Tooltip("Object that contains the physical gun.")]
    public GameObject gatlingGunObject;
    // cooldown vars
    private float currentCooldown;
    private bool waitingCooldown;
    private bool isFiringGun = false;
    private bool lastFiringGun = false;
    private Vector2 tmp;

    void Start()
    {
        SetGunEnabled(isEnabled);
    }

    void FixedUpdate()
    {
        if (waitingCooldown)
        {
            currentCooldown += Time.fixedDeltaTime;
            // turn off wait if the time is up
            if (currentCooldown >= fireRate)
            {
                waitingCooldown = false;
            }
        }
        lastFiringGun = isFiringGun;
        isFiringGun = false;
    }

    /// <summary>
    /// Fires a bullet
    /// </summary>
    public bool Fire(Vector3 bulletOrigin, Vector3 aimDirection)
    {
        if (!isEnabled) return false;
        isFiringGun = true;
        if (!waitingCooldown)
        {
            GameObject bullet = BulletPool.instance.GetObject();
            // clear bullet trail
            bullet.GetComponent<TrailRenderer>().Clear();
            bullet.transform.position = bulletOrigin;
            Bullet component = bullet.GetComponent<Bullet>();
            component.SetDirection(aimDirection);
            if (overrideBulletSpeed)
            {
                component.speed = bulletSpeed;
            }
            bullet.SetActive(true);
            // start cooldown
            waitingCooldown = true;
            currentCooldown = 0;
            return true;
        }
        return false;
    }

    public bool IsFiringGun
    {
        get
        {
            return this.lastFiringGun;
        }
    }

    public bool IsEnabled
    {
        get
        {
            return isEnabled;
        }
        set
        {
            isEnabled = value;
            // show/hide gun
            SetGunEnabled(isEnabled);
        }
    }

    /// <summary>
    /// Enables the object that holds the renderer for the gun
    /// </summary>
    /// <param name="enabled"></param>
    public void SetGunEnabled(bool enabled)
    {
        gatlingGunObject.SetActive(enabled);
    }
}
