using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour
{

    bool collected = false;

    private void OnCollisionTriggerEnter2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && collected == false)
        {
            collected = true;
            collision.gameObject.GetComponent<Player>().addDust();
            Destroy(this.gameObject);
        }
    }
}
