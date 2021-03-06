﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamStateManager : MonoBehaviour
{
    public int dustCost = 30;
    public float dreamStateTime = 30f;
    public bool inDreamState = false;

    public float timer;
    Player player;

    void Start()
    {
        player = GetComponent<Player>();
        timer = dreamStateTime;
    }


    void Update()
    {
        if(Input.GetButtonDown("Fire2") && !inDreamState)
        {
            if(player.getDust() >= dustCost)
            {
                player.addDust(-dustCost);
                activateDreamState();
            }
        }

        if(inDreamState)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                deactivateDreamState();
            }
        }
    }

    void activateDreamState()
    {
        inDreamState = true;
    }

    public void deactivateDreamState()
    {
        inDreamState = false;
        timer = dreamStateTime;
    }
}
