using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [Tooltip("How fast the player teleports.")]
    public float teleportSpeed = 40f;

    private GameObject teleportDummy;
    private bool dummyEnabled;
    private Vector3 tmp;

    /// <summary>
    /// Exact time when the teleport ended
    /// </summary>
    private float timeWhenTeleportEnded;

    void Start()
    {
        teleportDummy = new GameObject("Teleport Helper");
        teleportDummy.AddComponent<CharacterController>();
    }

    public void BeginTeleport(GameObject target)
    {
        // Automatically grab the soul that's closest to the player
        if (target != null)
        {
            // Set the dummy to the position of the soul so that we can use 
            // the position after the collision adjustements have been done
            UpdateDummyPosition(target.transform.position);
        }
    }

    public bool UpdateTeleport()
    {
        if (dummyEnabled)
        {
            // Disable immediately so that it doesn't collide with anything
            DisableDummy();
            dummyEnabled = false;
        }
        // Step by step move player torwards teleport destination
        float step = teleportSpeed * Time.fixedDeltaTime;
        Vector3 currentPosition = transform.position;
        Vector3 toPosition = teleportDummy.transform.position;
        Vector3 nextPosition = Vector3.MoveTowards(currentPosition, toPosition, step);
        transform.position = nextPosition;
        if (nextPosition == toPosition)
        {
            return true;
        }
        return false;
    }

    private void UpdateDummyPosition(Vector3 position)
    {
        teleportDummy.SetActive(true);
        teleportDummy.transform.position = position;
        // Move dummy into every direction to resolve collisions and let it readjust position if neccesary
        CharacterController characterController = teleportDummy.GetComponent<CharacterController>();
        tmp.Set(-0.0001f, -0.0001f, -0.0001f);
        characterController.Move(tmp);
        tmp.Set(0.0002f, 0.0002f, 0.0002f);
        characterController.Move(tmp);
        tmp.Set(-0.0001f, -0.0001f, -0.0001f);
        characterController.Move(tmp);
        dummyEnabled = true;
    }

    private void DisableDummy()
    {
        teleportDummy.SetActive(false);
    }
}
