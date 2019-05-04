using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float health = 100;
    public float maxHealth;
    [SerializeField]
    int lives = 5;
    [SerializeField]
    int dust = 0;

    HandleDeath handleDeath;

    private void Start()
    {
        maxHealth = health;
        handleDeath = GetComponent<HandleDeath>();
    }

    private void Update()
    {
        if(health <= 0)
        {
            takeLive();
            health = maxHealth;
            handleDeath.respawn();
        }
    }

    public void takeDamage(float amount)
    {
        health = health - amount;
    }

    public void takeLive()
    {
        lives--;
    }
    
    public void addDust(int amount = 1)
    {
        dust = dust + amount;
    }

    public float getHealth()
    {
        return health;
    }

    public int getLives()
    {
        return lives;
    }

    public int getDust()
    {
        return dust;
    }

}
