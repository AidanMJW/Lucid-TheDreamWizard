using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handleAnimation : MonoBehaviour
{
    SpriteRenderer sRender;
    Rigidbody2D rigBody;
    Animator anim;
    PlayerAttack pAttack;
    DreamStateManager dState;

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
        dState = GetComponent<DreamStateManager>();
    }


    void Update()
    {
        if(MenuManager.getPauseState() == false && pAttack.isFireing == false)
        {
            flipSprite();
        }
        attacking();

        if (dState.inDreamState)
            dreamState();
        else
        {
            anim.SetBool("dreamState", false);
            idleWalk();
        }
            

    }

    void flipSprite()
    {
        if( Input.GetAxis("Horizontal") > 0)
        {
            sRender.flipX = false;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            sRender.flipX = true;
        }
    }

    void dreamState()
    {
        anim.SetBool("dreamState", true);
        anim.SetBool("isWalking", false);
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
