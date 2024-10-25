using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{


    public float damage;

    [Range(1, 10)]
    private float speed = 15f;

    [Range(1, 10)]
    private float lifeTime = 5f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("topCollider"))
        {
            Destroy(gameObject);
        }
        
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.CompareTag("Deployable"))
        {
            Deployable deployable = collision.gameObject.GetComponent<Deployable>();
            deployable.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
