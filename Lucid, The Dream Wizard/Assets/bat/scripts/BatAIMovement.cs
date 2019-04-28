using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAIMovement : MonoBehaviour
{


    public float speed = 1;
    
    public LayerMask platformLayer;
    public LayerMask groundLayers;
   

    float speedHolder;
    GameObject player;
    Vector2 playerLocation;
    Rigidbody2D rigBody;
    Vector3 rotationRight;
    Vector3 rotationLeft;

    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigBody = GetComponent<Rigidbody2D>();
        speedHolder = speed;
        direction = rigBody.velocity;

        rotationRight = new Vector3(25,transform.eulerAngles.y,transform.eulerAngles.z );
        rotationLeft = new Vector3(-25, transform.eulerAngles.y, transform.eulerAngles.z);

    }

    // Update is called once per frame
    void Update()
    {
        
       
    }

    private void FixedUpdate()
    {
        playerLocation = player.transform.position;
        direction = rigBody.velocity;
        

        if (playerLocation.x > transform.position.x + 0.1)
        {
            direction.x = 1 * speed;
            transform.eulerAngles = rotationRight;
        }
        else if (playerLocation.x < transform.position.x - 0.1)
        {
            direction.x = -1 * speed;
            transform.eulerAngles = rotationLeft;
        }
        else
        {
            direction.x = 0;
        }

        if (playerLocation.y > transform.position.y + 0.1)
        {
            direction.y = 1 * speed;
        }
        else if (playerLocation.y < transform.position.y - 0.1)
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
