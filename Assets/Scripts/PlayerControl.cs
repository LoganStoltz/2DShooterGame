using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Player Vars
    [SerializeField] private string inputNameHorizontal;
    [SerializeField] private string inputNameVertical;
    [SerializeField] private Animator m_Animator;
    private bool moving = false;

    public float movSpeed;
    float speedX, speedY;
    Rigidbody2D rb;

    private Vector2 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_Animator = gameObject.GetComponent<Animator>(); 
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
        if(speedX == 0 && speedY == 0)
        {
            m_Animator.SetBool("moving", false);
        }
        else
        {
            m_Animator.SetBool("moving", true);
        }
    }
}
