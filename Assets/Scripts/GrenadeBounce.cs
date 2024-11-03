using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBounce : MonoBehaviour
{
    private Rigidbody2D rb;
    private float bounceDamping = 0.6f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reduce bounce velocity slightly for each bounce
        Vector2 bounceVelocity = rb.velocity;
        bounceVelocity.y *= -bounceDamping;
        rb.velocity = bounceVelocity;
    }
}

