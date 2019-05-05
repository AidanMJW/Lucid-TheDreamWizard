using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public bool checkPoint = false;
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        if(checkPoint == false)
        {
            if (player.transform.position.x >= transform.position.x)
                checkPoint = true;
        }

    }
}
