using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamStateManager : MonoBehaviour
{
    public int dustCost = 30;
    public float dreamStateTime = 30f;
    public bool inDreamState = false;

    float timer;
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
                timer = dreamStateTime;
            }
        }
    }

    void activateDreamState()
    {
        inDreamState = true;
    }

    void deactivateDreamState()
    {
        inDreamState = false;
    }
}
