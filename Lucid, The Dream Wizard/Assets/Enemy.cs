using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float health = 100;

    void Update()
    {
        if (health <= 0 || transform.position.y <= -50)
            die();
    }

    public void takeDamage(float amount)
    {
        float h = health;
        h -= amount;
        if (h < 0)
            h = 0;
        health = h;
    }

    void die()
    {
        Destroy(transform.gameObject);
    }
}
