using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public float jumpPower;
    public GameObject feetPosition;

    Rigidbody2D rigBody;
    bool isGrounded;

    void Start()
    {
        rigBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        checkIfGrounded();
    }

    void FixedUpdate()
    {
        walk();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jump();
        }
    }

    void walk()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal") * walkSpeed, rigBody.velocity.y);
        rigBody.velocity = direction;
    }

    void checkIfGrounded()
    {
        bool grounded = false;
        Collider2D[] coliders = Physics2D.OverlapCircleAll(feetPosition.transform.position, 0.1f);

        for (int i = 0; i < coliders.Length; i++)
        {
            if (coliders[i].gameObject.layer == 10)
            {
                grounded = true;
                break;
            }
        }

        isGrounded = grounded;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10)
            isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 10)
            isGrounded = false;
    }
    void jump()
    {
        Vector2 jump = new Vector2(rigBody.velocity.x, jumpPower);
        rigBody.velocity = jump;
    }

    public bool getGrounded()
    {
        return isGrounded;
    }
}
