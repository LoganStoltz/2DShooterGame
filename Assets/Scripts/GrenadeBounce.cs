using System.Collections;
using UnityEngine;

public class GrenadeBounce : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float initialBounceForce = 5f; // Initial bounce force
    [SerializeField] private float minimumBounceForce = 0.5f; // Minimum force threshold to stop bouncing
    [SerializeField] private float bounceScaleFactor = 1.2f; // Scale factor for visual bounce effect
    [SerializeField] private float scaleDamping = 8f; // Damping speed for returning to original scale

    private Vector3 originalScale;
    private float currentBounceForce; // Current bounce force that reduces each bounce
    private float bounceInterval = 1f; // Initial bounce interval
    private bool isBouncing = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
        currentBounceForce = initialBounceForce;

        // Start the timed bounce coroutine
        StartCoroutine(TimedBounces());
    }

    private IEnumerator TimedBounces()
    {
        while (isBouncing)
        {
            yield return new WaitForSeconds(bounceInterval);

            // Apply an upward force to simulate a bounce
            rb.velocity = new Vector2(rb.velocity.x, currentBounceForce);

            // Trigger the visual bounce effect
            StartCoroutine(BounceEffect());

            // Reduce the bounce force and interval for the next bounce
            currentBounceForce *= 0.5f; // Reduce force by half
            bounceInterval *= 0.5f; // Halve the interval time

            // Check if the bounce force is below the minimum threshold to stop bouncing
            if (currentBounceForce < minimumBounceForce)
            {
                isBouncing = false;
                rb.velocity = Vector2.zero; // Stop the grenade
            }
        }
    }

    private IEnumerator BounceEffect()
    {
        // Temporarily increase scale for the "hop" effect
        transform.localScale = originalScale * bounceScaleFactor;

        // Smoothly return to the original scale
        while (transform.localScale != originalScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * scaleDamping);
            yield return null;
        }
    }
}
