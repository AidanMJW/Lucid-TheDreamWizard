using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    int health = 5;
    [SerializeField]
    int dust = 0;

    public void takeDamage()
    {
        health = health - 1;
    }
    
    public void addDust(int amount = 1)
    {
        dust = dust + amount;
    }

    public int getHealth()
    {
        return health;
    }

    public int getDust()
    {
        return dust;
    }

}
