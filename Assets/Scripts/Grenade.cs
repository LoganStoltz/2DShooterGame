using System.Collections;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private float explosionDelay = 3f;   // Time before explosion
    [SerializeField] private float explosionRadius = 2f;  // Radius of explosion
    [SerializeField] private float explosionForce = 500f; // Force applied to nearby objects
    [SerializeField] private GameObject explosionEffect;  // Particle effect prefab for the explosion
    [SerializeField] private LayerMask damageLayer;       // Layer mask to identify objects that take damage

    private bool hasExploded = false;

    private void Start()
    {
        // Start the countdown to explosion
        StartCoroutine(ExplosionCountdown());
    }

    private IEnumerator ExplosionCountdown()
    {
        yield return new WaitForSeconds(explosionDelay);
        Explode();
    }

    private void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;

        // Show explosion effect if any
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Detect objects within the explosion radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, damageLayer);
        foreach (Collider2D nearbyObject in colliders)
        {
            Rigidbody2D rb = nearbyObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 forceDirection = (rb.position - (Vector2)transform.position).normalized;
                rb.AddForce(forceDirection * explosionForce);
            }
            // Apply damage if applicable
        }

        Destroy(gameObject); // Remove grenade object after explosion
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
