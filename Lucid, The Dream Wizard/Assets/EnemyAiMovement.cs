using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiMovement : MonoBehaviour
{
    public float speed = 1;
    public float jumpForce;
    public float jumpHeight;
    public LayerMask platformLayer;
    public Vector2 feetOffset;
    public Collider2D feetColider;
   

    GameObject player;
    Vector2 playerLocation;
    Rigidbody2D rigBody;
    bool isGrounded;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigBody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        playerLocation = player.transform.position;
        checkIfGrounded();

    }

    private void FixedUpdate()
    {
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
            direction.x = 1 * speed;
        else if (playerLocation.x < transform.position.x - 0.1)
            direction.x = -1 * speed;
        else
            direction.x = 0;

        rigBody.velocity = direction;
    }

    void jump()
    {
        if(jumpCheckVerticle())
        {
            rigBody.velocity = new Vector2(rigBody.velocity.x, jumpForce);
        }
    }

    void drop()
    {
        if(dropCheck())
        {
            feetColider.enabled = false;
            Debug.Log("droping");
        }
        else
        {
            if (isGrounded == false)
                feetColider.enabled = true;       
        }

    }

    void jumpCheckHorizontal()
    {

    }

    bool jumpCheckVerticle()
    {
        bool shouldJump = false;

        if(playerLocation.y > transform.position.y + 0.5f )
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up,1f, platformLayer);
            if(hit.collider!= null)
            {
                Debug.Log(hit.collider.name);
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
            if ( col.tag == "Platform" )
            {
                drop = true;
            }
        }

        return drop;
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
