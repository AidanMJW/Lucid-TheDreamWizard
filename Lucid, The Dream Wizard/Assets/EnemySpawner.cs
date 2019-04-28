using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int maxSpawnAmount = 3;
    public float respawnTimer = 30;
    public Vector3 spawnPos;

    bool respawn = false;
    float timer;

    public List<GameObject> Enemys = new List<GameObject>();
    public List<GameObject> activeEnemys = new List<GameObject>();

    private void Start()
    {
        timer = respawnTimer;
        spawnEnemys();
    }

    void Update()
    {
        checkActives();

        if (activeEnemys.Count == 0 && respawn == false)
            respawn = true;

        if(respawn == true)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                spawnEnemys();
                respawn = false;
                timer = respawnTimer;
            }
        }
    }

    void spawnEnemys()
    {
        int rand = Random.Range(1, maxSpawnAmount + 1);
        for(int i = 0; i < rand; i++ )
        {
            activeEnemys.Add(Instantiate(Enemys[Random.Range(0, Enemys.Count)]));

            activeEnemys[i].transform.position = new Vector3(spawnPos.x + i, spawnPos.y, 0.000001f);

        }
    }

    void checkActives()
    {
        for (int i = 0; i < activeEnemys.Count; i++)
            if (activeEnemys[i] == null)
                activeEnemys.RemoveAt(i);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(spawnPos, 0.1f);
    }

}
