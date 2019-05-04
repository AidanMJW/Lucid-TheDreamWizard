using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    Player player;
    public Image health;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();     
    }

    void Update()
    {

        health.fillAmount = (player.getHealth() / player.maxHealth);
        
    }
}
