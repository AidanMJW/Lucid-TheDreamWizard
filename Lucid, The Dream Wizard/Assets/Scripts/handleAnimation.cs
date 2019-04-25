using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handleAnimation : MonoBehaviour
{
    SpriteRenderer sRender;
    Rigidbody2D rigBody;
    Animator anim;
    PlayerAttack pAttack;

    bool isMoving;
    Vector3 pos;
    bool fired = false;

    void Start()
    {
        sRender = GetComponent<SpriteRenderer>();
        rigBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pos = transform.position;
        pAttack = GetComponent<PlayerAttack>();
    }


    void Update()
    {
        flipSprite();
        attacking();
        idleWalk();   
    }

    void flipSprite()
    {
        if(rigBody.velocity.x > 0 && Input.GetAxis("Horizontal") != 0)
        {
            sRender.flipX = false;
        }
        else if (rigBody.velocity.x < 0 && Input.GetAxis("Horizontal") != 0)
        {
            sRender.flipX = true;
        }
    }

    void idleWalk()
    {
        if (pos.x != transform.position.x && Input.GetAxis("Horizontal") != 0)
            isMoving = true;
        else
            isMoving = false;

        pos = transform.position;
        anim.SetBool("isWalking", isMoving);
    }

    void attacking()
    {
        if(pAttack.isFireing == true && fired == false)
        {
            anim.SetTrigger("isFireing");
            fired = true;
        }

        if(fired && pAttack.isFireing == false)
        {
            fired =false;
        }
    }
}
