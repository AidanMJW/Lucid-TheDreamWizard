using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiMovement : MonoBehaviour
{
    public float speed = 1;
    public float jumpForce;
    public float jumpHeight;
    public float followRange = 10f;
    public LayerMask platformLayer;
    public LayerMask groundLayers;
    public Vector2 feetOffset;
    public Collider2D feetColider;

    float speedHolder;
    public GameObject player;
    Vector2 playerLocation;
    public Rigidbody2D rigBody;
    public EnemyMeleeAttack enemyMelee;
    bool isGrounded;
    bool jumping;
    public float dir = 1f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyMelee = GetComponent<EnemyMeleeAttack>();
        rigBody = GetComponent<Rigidbody2D>();
        speedHolder = speed;
    }


    void Update()
    {
        playerLocation = player.transform.position;
        checkIfGrounded();

    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < followRange)
            move();
    }

    void move()
    {
        moveHorizontal();
        if(isGrounded)
            jump();

        drop();

    }

    void moveHorizontal()
    {
        Vector2 direction = rigBody.velocity;
        
        if (playerLocation.x > transform.position.x + 0.1)
        {
            direction.x = 1 * speed;
            dir = 1f;
        }
            
        else if (playerLocation.x < transform.position.x - 0.1)
        {
            direction.x = -1 * speed;
            dir = -1f;
        }
            
        else
            direction.x = 0;

        if (isGrounded)
        {
            if (landingSpotCheck(dir, 0.1f))
            {
                rigBody.velocity = direction;
            }
            else
            {
                direction.x = 0;
                rigBody.velocity = direction;
            }
        }
        else
        {
            rigBody.velocity = direction;
        }

        
    }

    void jump()
    {
        if(jumpCheckVerticle())
        {
            rigBody.velocity = new Vector2(rigBody.velocity.x, jumpForce);
        }

        if(jumpCheckHorizontal())
        {
            rigBody.velocity = new Vector2(rigBody.velocity.x, jumpForce );
            jumping = true;
        }

        if(jumping == true)
        {
            if(isGrounded)
            {
                speed = speedHolder * 2;
            }

            
            jumping = false;
            
        }
        else if(jumping == false)
        {
                speed = speedHolder;
        }

    }

    void drop()
    {
        if(dropCheck())
        {
            feetColider.enabled = false;
        }
        else
        {
            if (isGrounded == false)
                feetColider.enabled = true;       
        }

    }

    bool jumpCheckHorizontal()
    {
        bool jump = false;
        if (isGrounded)
        {
            

            Vector2 direction = Vector2.zero;
            if (transform.position.x > player.transform.position.x)
                direction = Vector2.left;
            else
                direction = Vector2.right;

            if(landingSpotCheck(direction.x,1))
            {
                Vector2 pos = transform.position;
                pos += feetOffset;
                pos += direction * 0.5f;

                Collider2D ground = Physics2D.OverlapCircle(pos, 0.2f);

                if (ground == null)
                {
                    jump = true;
                }
            }

        }
        return jump;
    }

    bool landingSpotCheck(float direction, float distance)
    {
        bool hasLandingSpot = false;

        Vector2 pos = transform.position;
        pos += feetOffset;

        pos.x += (distance * direction);

        Collider2D hit = Physics2D.OverlapCircle(pos, 0.1f);

        if(hit != null)
        {
            if (((1 << hit.gameObject.layer) & groundLayers) != 0)
            {
                hasLandingSpot = true;
            }
        }

        return hasLandingSpot;
    }

    bool jumpCheckVerticle()
    {
        bool shouldJump = false;

        if(playerLocation.y > transform.position.y + 0.5f && isGrounded )
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up,1f, platformLayer);
            if(hit.collider!= null)
            {
                shouldJump = true;
            }
        }
        return shouldJump;
    }

    bool dropCheck()
    {
        bool drop = false;
        if(player.transform.position.y < transform.position.y - 0.3)
        {
            Vector2 pos = transform.position;
            Collider2D col = Physics2D.OverlapCircle(pos + feetOffset, 0.1f);
            
            if ( col != null && col.tag == "Platform" )
            {
                if(underPlatformCheck())
                drop = true;
            }
        }

        return drop;
    }

    bool underPlatformCheck()
    {
        bool under = false;
        Vector2 pos = transform.position;
        Vector2 underPlatformPos = pos + feetOffset;
        underPlatformPos.y += -0.6f;
        RaycastHit2D hit = Physics2D.Raycast(underPlatformPos, Vector2.down);
        if(hit.collider != null)
        {
            under = true;
        }
        return under;
    }

    void checkIfGrounded()
    {
        bool grounded = false;
        Vector2 pos = transform.position;
        Collider2D[] coliders = Physics2D.OverlapCircleAll((pos + feetOffset) , 0.1f);

        for (int i = 0; i < coliders.Length; i++)
        {
            if (coliders[i].gameObject.layer == 10 || coliders[i].gameObject.layer == 11)
            {
                grounded = true;
                break;
            }
        }

        isGrounded = grounded;
    }
}
