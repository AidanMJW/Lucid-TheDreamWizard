using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDown : MonoBehaviour
{
    public Collider2D c;
    PlayerController pc;

    bool downPressed;

    List<Collider2D> platforms = new List<Collider2D>();
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

        if (Input.GetAxis("Vertical") >= 0)
            downPressed = false;

        enableColliders();
    }

    void disableColliders()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(pc.feetPosition.transform.position, 0.2f);
        foreach (Collider2D col in collider2Ds)
        {
            if (col.tag == "Platform")
            {
                platforms.Add(col);
                col.GetComponent<PlatformEffector2D>().rotationalOffset = 180;
            }
        }
    }

    void enableColliders()
    {
        if(platforms != null && platforms.Count > 0)
        {
            for(int i = 0; i < platforms.Count; i++)
            {
                if(platforms[i].transform.position.y > pc.feetPosition.transform.position.y + 0.5f)
                {
                    platforms[i].GetComponent<PlatformEffector2D>().rotationalOffset = 0;
                    platforms.RemoveAt(i);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Platform")
        {
            foreach(Collider2D col in platforms)
            {
                col.enabled = true;
            }
            platforms.Clear();
        }
    }
}
