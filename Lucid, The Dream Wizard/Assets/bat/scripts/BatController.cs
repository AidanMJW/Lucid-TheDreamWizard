using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    /*
     * Bat story:
     * a newly spawned bat will fly (chase) towards a co-ordinate that is a random distance behind and above the player.
     * Once at this waypoint it will wait a random time ( 0 - 1 second) before making an attack dive on the player. 
     * Once it contacts the player it will cause damage and retreat back to a random distance above and behind the player.
     * 
     * Bat state is used to select a behaviour for the bat
     * 
     */
    // an enumaration for referring to state
    public enum BatState
    {
        Chase,//fly (chase) towards a co-ordinate that is a random distance above the player.
        Wait,//stationary  wait a random time ( 0 - 1 second) before making an attack dive on the player
        Attack,//making an attack dive on the player.
        Inactive//dead not visible waiting to be reactived.
    };

    public BatState m_BatState;
    private Animator animator;
    public float attackRange = 0.5f;
    public float attackPower = 2f;

    private float timeDelay = 1.0f;
    private float nextDirectionChange = 0.0f;
    private bool swap;
    private AudioSource batAudio;
    public AudioClip batDamageClip;
    public AudioClip batFlyingClip;

    public float speed = 1;    
    //public LayerMask platformLayer;
    //public LayerMask groundLayers;   

    float speedHolder;
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
        animator = GetComponent<Animator>();
        GetComponent<CircleCollider2D>().radius = attackRange;
        player = GameObject.FindGameObjectWithTag("Player");
        rigBody = GetComponent<Rigidbody2D>();
        speedHolder = speed;
        direction = rigBody.velocity;

        rotationRight = new Vector3(25,transform.eulerAngles.y,transform.eulerAngles.z );
        rotationLeft = new Vector3(-25, transform.eulerAngles.y, transform.eulerAngles.z);
        rotationNone = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
        m_BatState = BatState.Wait;
        targetPosition = player.transform.position;
        playerLocation = targetPosition;
        swap = true;
        batAudio = GetComponent<AudioSource>();
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {           
            m_BatState = BatState.Attack;
            //Debug.Log("attack state");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            m_BatState = BatState.Chase;           
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Player")
        {
            other.gameObject.GetComponent<Player>().takeDamage(attackPower * DifficultyManager.getAttackMultiplier());
            batAudio.clip = batDamageClip;
            batAudio.Play();
            m_BatState = BatState.Chase;           
        } 
    }

    private void FixedUpdate()
    {
       
        switch (m_BatState)
        {
            case BatState.Chase:
                //  animator.Play("ghost-idle", 0);//we can set an animation
                if (Time.time > nextDirectionChange)
                {
                   
                    nextDirectionChange = Time.time + timeDelay;
                    //set a target position                   
                   if(swap)
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
                }
               
                break;
            case BatState.Attack:               
                targetPosition.x = player.transform.position.x;
                targetPosition.y = player.transform.position.y;                
                break;
            case BatState.Wait:
                if (Time.time > nextDirectionChange)
                {
                    batAudio.clip = batFlyingClip;
                    batAudio.Play();
                   

                    nextDirectionChange = Time.time + 3f;
                    //set a target position                   
                    
                     targetPosition.x = transform.position.x + Random.value;
                     targetPosition.y = transform.position.y - Random.value;                      
                  
                    m_BatState = BatState.Chase;

                }
                break;

        }//end switch

        playerLocation = player.transform.position;
        direction = rigBody.velocity;
        
        
        if (targetPosition.x > transform.position.x + 0.1)
        {
            direction.x = 1 * speed;           
            transform.eulerAngles = rotationRight;
        }
        else if (targetPosition.x < transform.position.x - 0.1 )
        {
            direction.x = -1 * speed;
            transform.eulerAngles = rotationLeft;
        }
        else
        {
            direction.x = 0;
            transform.eulerAngles = rotationNone;
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
