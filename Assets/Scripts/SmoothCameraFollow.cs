using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float damping; // how fast the screen cataches up with the player.

    public Transform target;

    private Vector3 vel = Vector3.zero;
    private void FixedUpdate() {
        if (target != null) {
            Vector3 targetPosition = target.position + offset;
            targetPosition.z = transform.position.z;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, damping);
        }
    }
}
