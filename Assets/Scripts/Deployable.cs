using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deployable : MonoBehaviour
{
    public int health = 5;  // Number of hits it can take

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject); // Destroy when health reaches zero
        }
    }
}
