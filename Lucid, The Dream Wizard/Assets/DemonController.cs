using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonController : MonoBehaviour
{

    public enum DemonState
    {
        Idle,//idle animation, no movement
        Attack,//attempt to match players y position and towards player pos but no further than X amount from home position, spawning fire breath or fireball depending on x distance to player
        Defend,//attempt to avoid player in Y axis fly up or down opposite of players 
        Inactive//no animation, sleeping
    };
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
    private float nextDirectionChange = 0.0f;
    private bool swap;
    private AudioSource batAudio;
    public AudioClip batDamageClip;
    public AudioClip batFlyingClip;
    SpriteRenderer sRenderer;
    public float speed = 1;

    float speedHolder;
    public GameObject projectile;
    public GameObject breath;
    GameObject player;
    Vector2 targetPosition;
    Vector2 playerLocation;
    Rigidbody2D rigBody;
    Vector3 rotationRight;
    Vector3 rotationLeft;
    Vector3 rotationNone;

    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        attackOffset = new Vector3(0.5f,0.2f,0f);
        flippedAttackOffset = new Vector3(0.5f, -0.2f, 0f);
        fireBreathattackOffset = new Vector3(-0.9f,-0.5f,0f);
        flippedfireBreathattackOffset = new Vector3(-0.9f,0.5f,0f);
        sRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
      //  GetComponent<CircleCollider2D>().radius = attackRange;
        player = GameObject.FindGameObjectWithTag("Player");
        rigBody = GetComponent<Rigidbody2D>();
        speedHolder = speed;
        direction = rigBody.velocity;

        rotationRight = new Vector3(25, transform.eulerAngles.y, transform.eulerAngles.z);
        rotationLeft = new Vector3(-25, transform.eulerAngles.y, transform.eulerAngles.z);
        rotationNone = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
        m_DemonState = DemonState.Inactive;
        targetPosition = player.transform.position;
        playerLocation = targetPosition;
        swap = true;
        //batAudio = GetComponent<AudioSource>();
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
        switch (m_DemonState)
        {
            case DemonState.Idle:
                animator.Play("demon-idle-animation", 0);//we can set an animation
                animator.speed = 0.75f;
                /*   if (Time.time > nextDirectionChange)
                   {

                       nextDirectionChange = Time.time + timeDelay;
                       //set a target position                   
                       if (swap)
                       {
                           targetPosition.x = player.transform.position.x + 0.1f + Random.value;
                           swap = !swap;
                       }
                       else
                       {
                           targetPosition.x = player.transform.position.x - 0.1f - Random.value;
                           swap = !swap;
                       }

                       targetPosition.y = player.transform.position.y + attackRange * 0.8f + Random.value;
                   }*/

                break;
            case DemonState.Attack:
                animator.Play("demon-attack-no-breath-animation", 0);
                timer += Time.deltaTime;
                if (timer > animationTimeDelay)
                {
                    if(Vector3.Distance(player.transform.position, transform.position) > 1.5f)
                    {
                        fireProjectile();
                    }
                    else
                    {
                        breathFire();
                    }

                    //fireProjectile();
                   
                    m_DemonState = DemonState.Idle;
                    timer = 0f;                   
                }
                    // targetPosition.x = player.transform.position.x;
                    targetPosition.y = player.transform.position.y;
                break;
            case DemonState.Inactive:
                animator.Play("demon-idle-animation", 0);
                animator.speed = 0;
              /*  if (Time.time > nextDirectionChange)
                {
                    //batAudio.clip = batFlyingClip;
                   // batAudio.Play();


                    nextDirectionChange = Time.time + 3f;
                    //set a target position                   

                    targetPosition.x = transform.position.x + Random.value;
                    targetPosition.y = transform.position.y - Random.value;

                  //  m_BatState = BatState.Chase;

                }*/
                break;

        }//end switch
    }

        private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
           // m_BatState = BatState.Attack;
            //Debug.Log("attack state");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
           // m_BatState = BatState.Chase;
        }
    }

    void breathFire()
    {
        GameObject p = Instantiate(breath);

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

        // p.GetComponent<Projectile>().direction = direction;
        // p.GetComponent<FireHit>().damage = attackPower;
        //p.GetComponentInChildren<FireHit>().damage = attackPower;
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

        p.GetComponent<Projectile>().direction = direction;
        p.GetComponent<Projectile>().damage = attackPower;
    }

}
