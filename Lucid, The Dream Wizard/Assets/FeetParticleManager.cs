using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetParticleManager : MonoBehaviour
{
    public GameObject weakPlatformParticle;
    PlayerController player;

    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (player.getGrounded())
            weakPlatformParticle.SetActive(true);
        else
            weakPlatformParticle.SetActive(false);
    }
}
