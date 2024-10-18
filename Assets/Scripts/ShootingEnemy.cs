using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [SerializeField] private float damage;
    public GameObject enemy;
    public Transform target;
    public float speed = 2f;
    public Vector2 distanceFromTarget;

    [Range(0.1f, 1f)]
    private float fireRate = 0.1f;
    private float fireTimer;
    [SerializeField] private Transform firingPoint;
    

    public float rotateSpeed = 0.0025f;
    private Rigidbody2D rb;

    public GameObject bulletPrefab; // Ammo
    public GameObject dropItemPrefab; // The item to be dropped
    public float dropChance = 0.8f; // 80% chance

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
    // If there's no target, find it
    if (!target) {
        GetTarget();
    }
    
    // If the target is found
    if (target) {
        RotateTowardsTarget();
        
        // Fire the bullet if the fireTimer reaches 0 or less
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0) {
            ShootTarget();
            fireTimer = 1f / fireRate;  // Reset the timer based on the fire rate
        }

        // Update distance from target
        distanceFromTarget = (target.position - enemy.transform.position);
        }
    }

    private void FixedUpdate() 
    {
        // Move Forwards
        rb.velocity = transform.up * speed;
    }

    private void RotateTowardsTarget() {
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed); // Will take time to rotate
    }


    private void GetTarget() 
    {
        if(GameObject.FindGameObjectWithTag("Player"))
            target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
        }
        else if(other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            DropItem();
            GameController.manager.IncreaseScore(1);
        }
    }

    private void DropItem()
    {
        float randomValue = Random.value;

        if (randomValue <= dropChance)
        {
            Instantiate(dropItemPrefab, transform.position, Quaternion.identity);
            Debug.Log("item dropped");
        }
    }

    private void ShootTarget()
    {
        if(distanceFromTarget.x < 10000f && distanceFromTarget.y < 10000f)
            Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
            Debug.Log("enemyShoting");
    }
}
