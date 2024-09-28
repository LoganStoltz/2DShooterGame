using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    // Gun Vars
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [Range(0.1f, 1f)]
    [SerializeField] private float fireRate = 0.5f;

    private float fireTimer;


    // Player Vars
    [SerializeField] private string inputNameHorizontal;
    [SerializeField] private string inputNameVertical;

    public float movSpeed;
    float speedX, speedY;
    Rigidbody2D rb;

    private Vector2 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        speedX = Input.GetAxisRaw(inputNameHorizontal) * movSpeed; 
        speedY = Input.GetAxisRaw(inputNameVertical) * movSpeed;
        rb.velocity = new Vector2(speedX, speedY);
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        transform.localRotation = Quaternion.Euler(0,0, angle); 

        if(Input.GetMouseButton(0) && fireTimer <= 0f) {
            Shoot();
            fireTimer = fireRate;
        }
        else {
            fireTimer -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
    }
}
