using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float health = 100;
    public float maxHealth;
    [SerializeField]
    int lives;
    [SerializeField]
    int dust = 0;

    HandleDeath handleDeath;

    private void Start()
    {
        lives = DifficultyManager.getLives();
        maxHealth = health;
        handleDeath = GetComponent<HandleDeath>();
    }

    private void Update()
    {
        if(lives > DifficultyManager.getLives())
            lives = DifficultyManager.getLives();

        if (health <= 0)
        {
            takeLive();
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
        health = maxHealth;
    }
    
    public void addDust(int amount = 1)
    {
        dust = dust + amount;
    }

    public void addHealh(float amount)
    {
        float h = health + amount;
        if (h > maxHealth)
            health = maxHealth;
        else
            health = h;
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
