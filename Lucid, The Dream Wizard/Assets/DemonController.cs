﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DemonController : MonoBehaviour
{

    public enum DemonState
    {
        Idle,//idle animation, no movement
        Attack,//attempt to match players y position spawning fire breath or fireball depending on x distance to player
        Inactive//no animation, sleeping
    };
    private AudioSource demonAudio;
    public AudioClip demonActiveClip;
    private Vector3 attackOffset;
    private Vector3 flippedAttackOffset;
    private Vector3 fireBreathattackOffset;
    private Vector3 flippedfireBreathattackOffset;
    public DemonState m_DemonState;
    private Animator animator;
    public float attackRange = 0.5f;
    public float attackPower = 2f;
    private float animationTimeDelay = 1.1f;
    private float timer = 0.0f;
   
    private bool evasive = false;//flag that allows us to ignore current state and just evade projectile
    SpriteRenderer sRenderer;

    public float speed = 1;
    float speedHolder;
    public GameObject dragonsLair;
    public GameObject projectile;
    public GameObject breathfire;
    GameObject player;
    Vector2 targetPosition;
    Vector2 playerLocation;
    Rigidbody2D rigBody;
    // Vector3 rotationRight;
    // Vector3 rotationLeft;
    // Vector3 rotationNone;
    GameObject incomingProjectile = null;
    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        //where the projectiles and fire are instantiated
        attackOffset = new Vector3(0.7f,0.2f,0f);
        flippedAttackOffset = new Vector3(0.7f, -0.2f, 0f);
        fireBreathattackOffset = new Vector3(-0.9f,-0.5f,0f);
        flippedfireBreathattackOffset = new Vector3(-0.9f,0.5f,0f);

        sRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
      
        player = GameObject.FindGameObjectWithTag("Player");
        rigBody = GetComponent<Rigidbody2D>();
        speedHolder = speed;
        direction = rigBody.velocity;

       
        m_DemonState = DemonState.Inactive;
        targetPosition = dragonsLair.transform.position;
        
        demonAudio = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
       if (player.transform.position.x > transform.position.x)
        {
            sRenderer.flipX = true;
        }
        else
        {
            sRenderer.flipX = false;
        }
       if(!evasive)
       {
            switch (m_DemonState)
            {
                case DemonState.Idle:
                    animator.Play("demon-idle-animation", 0);//we can set an animation
                    animator.speed = 0.9f;
                    speed = 1.2f;                  
                    targetPosition = dragonsLair.transform.position;
                    break;
                case DemonState.Attack:

                    speed = 1.8f;
                    targetPosition.y = player.transform.position.y + 0.3f;
                    targetPosition.x = transform.position.x;

                    animator.Play("demon-attack-no-breath-animation", 0);
                    animator.speed = 0.8f;
                    timer += Time.deltaTime;
                    if (timer > animationTimeDelay)//gives enough for animation to complete
                    {
                        if (Vector3.Distance(player.transform.position, transform.position) > 1.25f)
                        {
                            fireProjectile();
                        }
                        else
                        {
                            breathFire();
                        }
                        timer = 0f;
                    }
                break;
                case DemonState.Inactive:
                    animator.Play("demon-idle-animation", 0);
                    animator.speed = 0f;
                    speed = 0.8f;                   
                    targetPosition = dragonsLair.transform.position;
                 break;

            }//end switch

        }
        else
        {
            //check x transform of projectile
            if(incomingProjectile != null)
            {
                animator.Play("demon-idle-animation", 0);
                animator.speed = 1f;
                if (System.Math.Abs(incomingProjectile.transform.position.x - transform.position.x) > 3)
                {
                    evasive = false;
                }
            }
            else
            {
                evasive = false;
            }
        }
        move();
    }//end fixed update

    public void awaken()
    {
        m_DemonState = DemonState.Idle;
        //play audio
        demonAudio.clip = demonActiveClip;
        demonAudio.Play();
    }

    public void avoid(GameObject go)
    {
       
        /*
         * get the transform of incoming go - Lucid fires horizontally so we are interested its y value
         * If distance of fireball y transform to our lowest y co-ord of our collider 
         * is less than distance to our highest y co-ord of our collider we want to move up else move down. 
         */
        targetPosition.x = transform.position.x;//just move up and down for now
        float incomingfireball_y = go.transform.position.y;
        float myLowest_y = transform.position.y - 0.5f;
        float myHighest_y = transform.position.y + 0.5f;
        if(incomingfireball_y < myHighest_y && incomingfireball_y > myLowest_y)//we gottsta move
        {
            evasive = true;
            incomingProjectile = go;
            animator.Play("demon-idle-animation", 0);
            animator.speed = 1f;
            int chance = Random.Range(0, 100);
           
                // Debug.Log("avoid");
            if (myHighest_y - incomingfireball_y < incomingfireball_y - myLowest_y)//move down
            {
               // Debug.Log("avoid - down");
                targetPosition.y = transform.position.y - 0.5f;
                speed = 3.0f;
            }
            else //move up
            {
                //Debug.Log("avoid - up");
                targetPosition.y = transform.position.y + 1f;
                speed = 3.0f;
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {       
        if (other.gameObject.name == "landCollider(Clone)")
        {
            //move up so we aren't trapped against ground
            targetPosition.y = transform.position.y + 1f;
            speed = 3.0f;
            move();
        }
    }

    private void move()
    {
        direction = rigBody.velocity;
        if (targetPosition.x > transform.position.x + 0.1)
        {
            direction.x = 1 * speed;
            //transform.eulerAngles = rotationRight;
        }
        else if (targetPosition.x < transform.position.x - 0.1)
        {
            direction.x = -1 * speed;
            //transform.eulerAngles = rotationLeft;
        }
        else
        {
            direction.x = 0;
            //transform.eulerAngles = rotationNone;
        }

        if (targetPosition.y > transform.position.y + 0.1)
        {
            direction.y = 1 * speed;
        }
        else if (targetPosition.y < transform.position.y - 0.1)
        {
            direction.y = -1 * speed;
        }
        else
        {
            direction.y = 0;
        }
        rigBody.velocity = direction;
    }
    void breathFire()
    {
        GameObject p = Instantiate(breathfire);

        if (sRenderer.flipX == true)
        {
            direction = Vector3.left;
            p.GetComponent<SpriteRenderer>().flipX = true;
            p.transform.position = (transform.position + (flippedfireBreathattackOffset * direction.x));
        }
        else
        {
            direction = Vector3.right;
            p.transform.position = (transform.position + (fireBreathattackOffset * direction.x));
        }

      //  p.GetComponent<breathfire>().tag = "projectile";
        p.tag = "projectile";
    }
    void fireProjectile()
    {

        GameObject p = Instantiate(projectile);

        if (sRenderer.flipX == false)
        {
            direction = Vector3.left;
            p.GetComponent<SpriteRenderer>().flipX = true;
            p.transform.position = (transform.position + (attackOffset * direction.x));
        }
        else
        {
            direction = Vector3.right;
            p.transform.position = (transform.position + (flippedAttackOffset * direction.x));
        }
        p.GetComponent<Projectile>().tag = "projectile";
        p.tag = "projectile";
        p.GetComponent<Projectile>().direction = direction;
        p.GetComponent<Projectile>().damage = attackPower;
    }

}
