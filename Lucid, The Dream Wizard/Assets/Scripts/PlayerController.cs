﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public float jumpPower;
    public GameObject feetPosition;
    public LayerMask canJumpLayers;

    Rigidbody2D rigBody;
    bool isGrounded;
    bool doJump = false;

    void Start()
    {
        rigBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        checkIfGrounded();
        checkIfGroundedRaycast();

        if (Input.GetButtonDown("Jump")  && isGrounded )
        {
            doJump = true;
        }
    }

    void FixedUpdate()
    {
        walk();

        if (doJump)
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

        for (int i = 0; i < coliders.Length; i++)
        {
            if (((1 << coliders[i].gameObject.layer) & canJumpLayers) != 0)
            {
                grounded = true;
                break;
            }
        }

        isGrounded = grounded;
    }

    void checkIfGroundedRaycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up,0.3f);

        if (hit.collider != null)
        {
            if(hit.collider.gameObject.layer == canJumpLayers)
            isGrounded = true;
        }
         
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & canJumpLayers) != 0)
            isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & canJumpLayers) != 0)
            isGrounded = false;
    }
    void jump()
    {
        Vector2 jump = new Vector2(rigBody.velocity.x, jumpPower);
        rigBody.velocity = jump;
        doJump = false;
    }

    public bool getGrounded()
    {
        return isGrounded;
    }
}