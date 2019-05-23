using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWaves : MonoBehaviour
{
    public List<GameObject> Enemies = new List<GameObject>();
    public List<GameObject> spawnPoints = new List<GameObject>();
    public Vector2Int amountOfEnemiesRange = new Vector2Int();
    public GameObject shield;
    public bool playerAtBoss = false;
    public GameObject boss;
    bool waveActive;

    List<GameObject> activeEnemies = new List<GameObject>();
    
    public float activateTime;
    float timer =0;
    int count = 0;

    void Start()
    {
        
    }


    void Update()
    {      

        checkActives();

        if (boss != null && boss.GetComponent<DemonController>().m_DemonState != DemonController.DemonState.Inactive)
            playerAtBoss = true;

        if (playerAtBoss && !waveActive)
        {
            timer -= Time.deltaTime;
            Debug.Log("test");

            if(timer <= 0)
            {
                spawnWave();
                timer = activateTime;
            }
        }

        if(waveActive && activeEnemies.Count == 0)
        {
            shield.SetActive(false);
            waveActive = false;
        }
    }

    void spawnEnemy()
    {
        if (count == spawnPoints.Count)
            count = 0;

        activeEnemies.Add(Instantiate(Enemies[Random.Range(0, Enemies.Count)]));
        activeEnemies[activeEnemies.Count -1].transform.position = spawnPoints[count].transform.position;

        count++;
    }

    void spawnWave()
    {
        shield.SetActive(true);

        int amount = Random.Range(amountOfEnemiesRange.x, amountOfEnemiesRange.y + 1);

        for(int i = 0; i < amount; i++)
        {
            spawnEnemy();
        }

        waveActive = true;
    }

    void checkActives()
    {
        for (int i = 0; i < activeEnemies.Count; i++)
            if (activeEnemies[i] == null)
                activeEnemies.RemoveAt(i);
    }
}
