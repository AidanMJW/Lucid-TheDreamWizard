using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    bool collected = false;
    public float healthAmount = 25f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && collected == false)
        {
            collected = true;
            collision.gameObject.GetComponent<Player>().addHealh(healthAmount);
            Destroy(this.gameObject); 
        }
    }
}
