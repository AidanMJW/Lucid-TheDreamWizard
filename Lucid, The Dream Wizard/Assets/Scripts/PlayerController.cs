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

    void FixedUpdate()
    {
        checkIfGrounded();
        walk();
        jump();
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
        foreach(Collider2D c in coliders)
        {
            if (c.gameObject.layer == 10)
                grounded = true;
        }

        isGrounded = grounded;
    }

    void jump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            Vector2 jump = new Vector2(rigBody.velocity.x, jumpPower);
            rigBody.velocity = jump;
        }
    }

    public bool getGrounded()
    {
        return isGrounded;
    }
}
