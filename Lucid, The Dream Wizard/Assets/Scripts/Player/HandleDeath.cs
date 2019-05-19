using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleDeath : MonoBehaviour
{
    public float deathDepth = -50f;
    Player player;
    Rigidbody2D rigBody;
    GameObject[] respawnPoints;

    void Start()
    {
        player = GetComponent<Player>();
        rigBody = GetComponent<Rigidbody2D>();
        respawnPoints = GameObject.FindGameObjectsWithTag("Respawn");

        
    }

    private void Update()
    {
        if (player.getLives() <= 0)
            death();

        if (transform.position.y <= deathDepth)
        {
            player.takeLive();
            respawn();
        }
    }

    void death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void respawn()
    {
        float distance = 10000000f;
        Vector3 respawnPos = Vector2.zero;

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
