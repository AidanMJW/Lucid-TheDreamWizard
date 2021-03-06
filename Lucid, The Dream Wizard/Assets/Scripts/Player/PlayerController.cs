﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public float jumpPower;
    public float flySpeed;
    public GameObject feetPosition;
    public GameObject platformDown;
    public LayerMask canJumpLayers;

    DreamStateManager dreamState;
    Rigidbody2D rigBody;
    PlayerAttack pAttack;
    bool isGrounded;
    bool doJump = false;

    private AudioSource playerAudio;
    public AudioClip playerJumpClip;


    void Start()
    {
        rigBody = GetComponent<Rigidbody2D>();
        pAttack = GetComponent<PlayerAttack>();
        dreamState = GetComponent<DreamStateManager>();
        playerAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        checkIfGrounded();
        checkIfGroundedRaycast();

        if(MenuManager.getPauseState() == false)
        {
            if (Input.GetButtonDown("Jump") && isGrounded /*&& pAttack.isFireing == false*/)
            {
                doJump = true;
            }
        }

    }

    void FixedUpdate()
    {
        if(!dreamState.inDreamState)
        {
            rigBody.gravityScale = 2;
            feetPosition.SetActive(false);
            platformDown.SetActive(true);

            if (MenuManager.getPauseState() == false)
            {
                walk();
            }

            if (doJump)
                jump();
        }
        else
        {
            rigBody.gravityScale = 0;
            feetPosition.SetActive(true);
            platformDown.SetActive(false);
            fly();
        }

    }

    void walk()
    {
        if(pAttack.isFireing == false || !isGrounded)
        {
            Vector2 direction = new Vector2(Input.GetAxis("Horizontal") * walkSpeed, rigBody.velocity.y);
            rigBody.velocity = direction;
        }
        else
        {
            Vector2 newVelocity = rigBody.velocity;
            newVelocity.x = 0;
            rigBody.velocity = newVelocity;
        }

    }

    void fly()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal") * flySpeed, Input.GetAxis("Vertical") * flySpeed);
        rigBody.velocity = direction;
        
    }

    void checkIfGrounded()
    {
        bool grounded = false;
        Collider2D[] coliders = Physics2D.OverlapCircleAll(feetPosition.transform.position, 0.1f);

        for (int i = 0; i < coliders.Length; i++)
        {
            //activate weak platform
            if (coliders[i].gameObject.tag == "WeakPlatform" && !dreamState.inDreamState)
                coliders[i].gameObject.GetComponent<PlatformShake>().setBreak();

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
        playerAudio.clip = playerJumpClip;
        playerAudio.Play();
    }

    public bool getGrounded()
    {
        return isGrounded;
    }
}
