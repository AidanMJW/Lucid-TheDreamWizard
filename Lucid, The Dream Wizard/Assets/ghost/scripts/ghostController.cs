using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostController : MonoBehaviour
{
    /*
      * ghost story:
      * a newly spawned ghost will fly towards the player
      * Once it contacts the player it will stick to the player position for random time causing slow damage
      * After a the random attack time it will unstick and set a random target position away from the player
      * On reaching the random target position it will play the de-spawning animation and go to inactive state for a random amount of time(long time) before going active 
      * ghost state is used to select a behaviour for the ghost
      * 
      */
    // an enumaration for referring to state
    public enum GhostState
    {
        Spawning, //play a spawning animation a random distance from the player no pursuit of player
        Chase,//chase towards the player.
        Attack,//making higher speed attack run at the player.
        Despawning,//play a de-spawning animation no pursuit of player

        Inactive//dead not visible waiting to be reactived.
    };

    public GhostState m_GhostState;
    private Animator animator;
    public float attackRange = 0.5f;
    public float attackPower = 0.0001f;

    private float animationTimeDelay = 1.0f;
    public float inactiveTime = 10f;
    private float timer = 0.0f;

   
    private AudioSource ghostAudio;
    public AudioClip ghostDamageClip;
    public AudioClip ghostSpawningClip;

    public float speed = 0.35f;
    public float attackSpeed = 1f;
    float speedHolder = 0.35f;

    GameObject player;
    Vector2 targetPosition;
    Vector2 playerLocation;
    Rigidbody2D rigBody;
    Vector2 direction;

   
    private Animation anim;

    // Use this for initialization
    void Start()
    {
        m_GhostState = GhostState.Spawning;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        rigBody = GetComponent<Rigidbody2D>();
        speed = speedHolder;
        attackSpeed = player.GetComponent<PlayerController>().walkSpeed * 0.75f;
        attackPower = 0.05f;
        direction = rigBody.velocity;

        targetPosition = player.transform.position;
       
        //batAudio = GetComponent<AudioSource>();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player" && m_GhostState != GhostState.Inactive)
        {
            timer = 0f;
            m_GhostState = GhostState.Attack;
            speed = attackSpeed;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            timer = 0f;
            m_GhostState = GhostState.Despawning;
            speed = speedHolder;
        }
    }

    
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.name == "Player" &&  m_GhostState != GhostState.Inactive)
        {
           // Debug.Log("m_GhostState="+ m_GhostState);
            other.gameObject.GetComponent<Player>().takeDamage(attackPower * DifficultyManager.getAttackMultiplier());
        }
    }
    

    private void FixedUpdate()
    {       
        timer += Time.deltaTime;
        switch (m_GhostState)
        {
            case GhostState.Chase:
                animator.Play("ghost-Idle", 0);//we can set an animation              
                targetPosition.x = player.transform.position.x;
                targetPosition.y = player.transform.position.y;


                break;
            case GhostState.Attack:
                animator.Play("ghost-Attack", 0);//we can set an animation
                targetPosition.x = player.transform.position.x;
                targetPosition.y = player.transform.position.y;

                break;

            case GhostState.Spawning:
                animator.Play("ghost-Spawn", 0);//we can set an animation
                if (timer > animationTimeDelay)
                {
                    timer = 0f;
                    //batAudio.clip = batFlyingClip;
                    //batAudio.Play();
                    m_GhostState = GhostState.Chase;
                    GetComponent<Enemy>().active = true;

                }

                break;
            case GhostState.Despawning:
                animator.Play("ghost-despawn", 0);//we can set an animation
                if (timer > animationTimeDelay) { 
                   m_GhostState = GhostState.Inactive;
                    GetComponent<Enemy>().active = false;
                    timer = 0f;
                }

                break;
            case GhostState.Inactive:
                transform.position = new Vector3(transform.position.x, transform.position.y,-100);
                if (timer > 8f)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y,1);
                    m_GhostState = GhostState.Spawning;
                    timer = 0f;
                }

                break;

        }//end switch

      
        playerLocation = player.transform.position;

        direction = rigBody.velocity;


        if (targetPosition.x > transform.position.x + 0.1)
        {
            direction.x = 1 * speed;

        }
        else if (targetPosition.x < transform.position.x - 0.1)
        {
            direction.x = -1 * speed;
           
        }
        else
        {
            direction.x = 0;
            
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



}