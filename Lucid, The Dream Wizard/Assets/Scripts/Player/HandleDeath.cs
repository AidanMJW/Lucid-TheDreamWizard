using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleDeath : MonoBehaviour
{
    public float deathDepth = -50f;
    DreamStateManager stateManager;
    Player player;
    Rigidbody2D rigBody;
    GameObject[] respawnPoints;

    public MenuManager mManager;

    private bool audioPlayed = false;
    private AudioSource playerAudio;
    public AudioClip playerFallClip;
    public AudioClip playerDeathClip;


    void Start()
    {
        stateManager = GetComponent<DreamStateManager>();
        player = GetComponent<Player>();
        rigBody = GetComponent<Rigidbody2D>();
        respawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
        playerAudio = GetComponent<AudioSource>();

        
    }

    private void Update()
    {
        if (player.getLives() <= 0)
        {
           
            if (!playerAudio.isPlaying && !audioPlayed)
            {                
                playerAudio.clip = playerDeathClip;
                playerAudio.Play();
                audioPlayed = true;
                //player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 100);
                player.GetComponent<SpriteRenderer>().enabled = false;
                player.GetComponent<CapsuleCollider2D>().enabled = false;
            }

            death();

        }
        else
        {
            if (transform.position.y <= deathDepth)
            {
                playerAudio.clip = playerFallClip;
                playerAudio.Play();
                player.takeLive();
                respawn();
            }
        }
           

      
    }

    void death()
    {
        mManager.toggleDeathPanel();
     
    }

    public void respawn()
    {
        float distance = 10000000f;
        Vector3 respawnPos = Vector2.zero;
        stateManager.deactivateDreamState();

        for(int i = 0; i < respawnPoints.Length; i++)
        {
            if(Vector3.Distance(transform.position,respawnPoints[i].transform.position) < distance)
            {
                if(respawnPoints[i].GetComponent<RespawnPoint>().checkPoint)
                {
                    distance = Vector3.Distance(transform.position, respawnPoints[i].transform.position);
                    respawnPos = respawnPoints[i].transform.position;
                }
            }
        }

        rigBody.velocity = Vector3.zero;
        transform.position = respawnPos;    
    }

}
