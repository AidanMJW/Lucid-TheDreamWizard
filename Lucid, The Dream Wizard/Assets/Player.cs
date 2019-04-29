using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float health = 100f;
    [SerializeField]
    int dust =0;

    


    public void addDust(int amount = 1)
    {
        dust = dust + amount;
    }


}
