using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailShot : MonoBehaviour
{
    public Transform firingPoint;
    public GameObject RailgunEffectPrefab;
    public int maxCollisions = 5;

    public void FireRailgun()
    {
        if (firingPoint == null)
        {
            Debug.LogWarning("Firing point not set for RailShot!");
            return;
        }

        RaycastHit2D[] hits = Physics2D.RaycastAll(firingPoint.position, firingPoint.up, Mathf.Infinity);
        int collisionCount = 0;

        Vector3 endPosition = firingPoint.position + firingPoint.up * 100; // Default end point

        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Enemy") || hit.collider.CompareTag("EnemyBullet"))
            {
                Debug.Log("Enemy Hit");
                Destroy(hit.collider.gameObject);
                collisionCount++;

                if (collisionCount >= maxCollisions)
                {
                    endPosition = hit.point;
                    break;
                }
            }
        }

        if (RailgunEffectPrefab)
        {
            var railgunEffect = Instantiate(RailgunEffectPrefab, firingPoint.position, firingPoint.rotation);
            LineRenderer lineRenderer = railgunEffect.GetComponent<LineRenderer>();
            if (lineRenderer)
            {
                lineRenderer.SetPosition(0, firingPoint.position);
                lineRenderer.SetPosition(1, endPosition);
            }
            Destroy(railgunEffect, 0.1f); // Clean up the effect after a short delay
        }
    }
}
