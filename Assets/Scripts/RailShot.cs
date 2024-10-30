using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailShot : MonoBehaviour
{
    public int collisionCount = 0;
    public int maxCollisions = 5; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyBullet"))
        {
            collisionCount++;
            Destroy(other.gameObject);

            if (collisionCount >= maxCollisions)
            {
                Destroy(gameObject);
            }
        }
    }
}
