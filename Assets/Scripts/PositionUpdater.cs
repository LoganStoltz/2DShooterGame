using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionUpdater : MonoBehaviour
{
    public Transform target;
    public Vector2 offset;

    private void Update()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position;
            Vector3 updatedPosition = desiredPosition + (Vector3)offset;
            transform.position = updatedPosition;
        }
        else
        {
            Debug.LogWarning("Target Transform not set.");
        }
    }
}

