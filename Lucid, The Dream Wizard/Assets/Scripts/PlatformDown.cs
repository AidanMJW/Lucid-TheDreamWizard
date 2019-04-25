using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDown : MonoBehaviour
{
    public Collider2D c;
    public Collider2D feet;
    public LayerMask platform;
    PlayerController pc;

    bool downPressed;

    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }


    void Update()
    {
        if(pc.getGrounded())
        {
            if(Input.GetButtonDown("Vertical") || Input.GetAxis("Vertical") < -0.5f && downPressed == false)
            {
                downPressed = true;
                disableColliders();
            }
        }
        else
        {
            enableColliders();
        }

        if (Input.GetAxis("Vertical") >= 0)
            downPressed = false;


    }

    void disableColliders()
    {
        feet.enabled = false;
    }

    void enableColliders()
    {
        feet.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Platform" && other.gameObject.layer == platform)
        {
            enableColliders();
        }
    }
}
