using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float damage;
    public Transform target;
    public float speed = 3f;

    public float rotateSpeed = 0.0025f;
    private Rigidbody2D rb;

    public GameObject dropItemPrefab; // The item to be dropped
    public float dropChance = 0.1f; // 10% chance

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        // Get the target
        if (!target) {
            GetTarget();
        }
        // Rotate towards the target
        else {
            RotateTowardsTarget();
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
        else if(other.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(other.gameObject);
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
}
